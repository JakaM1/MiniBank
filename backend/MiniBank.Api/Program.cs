using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using MiniBank.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddDbContext<BankDb>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("MiniBank")
    ?? "Host=localhost;Database=minibank;Username=minibank"));
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddDataProtection().UseEphemeralDataProtectionProvider();
builder.Services.AddSingleton<SessionStore>();
builder.Services.AddSingleton<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
builder.Services.AddAntiforgery(o => { o.HeaderName = "X-CSRF-TOKEN"; o.Cookie.SameSite = SameSiteMode.Strict; });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
{
    o.Cookie.Name = "MiniBank.Auth";
    o.Cookie.HttpOnly = true;
    o.Cookie.SameSite = SameSiteMode.Lax;
    o.Cookie.SecurePolicy = builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Testing")
        ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
    o.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    o.SlidingExpiration = true;
    o.Events.OnRedirectToLogin = c => { c.Response.StatusCode = 401; return Task.CompletedTask; };
    o.Events.OnRedirectToAccessDenied = c => { c.Response.StatusCode = 403; return Task.CompletedTask; };
    o.Events.OnValidatePrincipal = async c =>
    {
        var principal = c.Principal!;
        var id = Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await c.HttpContext.RequestServices.GetRequiredService<BankDb>().Users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        if (user is null || !user.Active || user.Role != principal.FindFirstValue(ClaimTypes.Role)
            || user.SecurityStamp.ToString() != principal.FindFirstValue("stamp"))
        { c.RejectPrincipal(); await c.HttpContext.SignOutAsync(); }
    };
});
builder.Services.AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
    .Configure<SessionStore>((o, store) => o.SessionStore = store);
builder.Services.AddAuthorization();
var app = builder.Build();
if (!app.Environment.IsDevelopment() && !app.Environment.IsEnvironment("Testing")) app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (ctx, next) =>
{
    ctx.Response.OnStarting(() => { ctx.Response.Headers.CacheControl = "no-store"; return Task.CompletedTask; });
    if (HttpMethods.IsPost(ctx.Request.Method) || HttpMethods.IsPatch(ctx.Request.Method) || HttpMethods.IsDelete(ctx.Request.Method) || HttpMethods.IsPut(ctx.Request.Method))
    {
        try { await ctx.RequestServices.GetRequiredService<IAntiforgery>().ValidateRequestAsync(ctx); }
        catch (AntiforgeryValidationException) { ctx.Response.StatusCode = 400; await ctx.Response.WriteAsJsonAsync(new { code = "CSRF", message = "Neveljavna zaščita zahteve", fieldErrors = Array.Empty<string>() }); return; }
    }
    await next();
});
app.MapGet("/api/auth/csrf", (HttpContext ctx, IAntiforgery csrf) => Results.Ok(new { token = csrf.GetAndStoreTokens(ctx).RequestToken }));
app.MapPost("/api/auth/login", async (Login input, BankDb db, IPasswordHasher<AppUser> hasher, HttpContext ctx) =>
{
    var email = (input.Email ?? "").Trim().ToLowerInvariant();
    var user = await db.Users.SingleOrDefaultAsync(x => x.Email == email);
    var candidate = user ?? new AppUser();
    // Tudi neobstoječ uporabnik opravi preverjanje zgoščenega gesla.
    var hash = user?.PasswordHash ?? DummyPassword.Hash;
    var valid = hasher.VerifyHashedPassword(candidate, hash, input.Password ?? "") != PasswordVerificationResult.Failed;
    if (!valid || user is null || !user.Active)
        return Results.Json(new { code = "LOGIN_FAILED", message = "Prijava ni uspela", fieldErrors = Array.Empty<string>() }, statusCode: 401);
    await ctx.SignOutAsync();
    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Name, user.Name), new Claim(ClaimTypes.Role, user.Role), new Claim("stamp", user.SecurityStamp.ToString()) }, CookieAuthenticationDefaults.AuthenticationScheme);
    await ctx.SignInAsync(new ClaimsPrincipal(identity));
    return Results.Ok(new { user.Id, user.Name, user.Role });
});
app.MapPost("/api/auth/logout", async (HttpContext ctx) => { await ctx.SignOutAsync(); return Results.NoContent(); }).RequireAuthorization();
app.MapGet("/api/auth/me", (ClaimsPrincipal user) => Results.Ok(new { id = user.FindFirstValue(ClaimTypes.NameIdentifier), name = user.Identity!.Name, role = user.FindFirstValue(ClaimTypes.Role) })).RequireAuthorization();
var banking = app.MapGroup("/api/accounts").RequireAuthorization(p => p.RequireRole("CLIENT"));
banking.MapGet("", async (ClaimsPrincipal user, BankDb db) =>
{
    var id = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
    var accounts = await db.Accounts.AsNoTracking().Where(x => x.OwnerId == id).OrderBy(x => x.Number).ToListAsync();
    return Results.Ok(accounts.Select(x => new { x.Id, x.Number, x.Label, balance = Money(x.Balance), currency = "EUR" }));
});
banking.MapGet("/{id:guid}/transactions", async (Guid id, ClaimsPrincipal user, BankDb db) =>
{
    var owner = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
    if (!await db.Accounts.AnyAsync(x => x.Id == id && x.OwnerId == owner)) return Results.NotFound();
    var transfers = await db.Transfers.AsNoTracking().Where(x => x.Status == "EXECUTED" && (x.SourceAccountId == id || x.TargetAccountId == id))
        .OrderByDescending(x => x.ExecutedAt).ThenByDescending(x => x.Id).Take(20).ToListAsync();
    return Results.Ok(transfers.Select(x => new { x.Id, amount = Money(x.Amount), direction = x.SourceAccountId == id ? "OUT" : "IN", x.Purpose, date = x.ExecutedAt }));
});
if (app.Environment.IsDevelopment() && builder.Configuration.GetValue<bool>("Seed:Enabled"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BankDb>();
    await db.Database.MigrateAsync();
    await DevelopmentSeed.Run(db, builder.Configuration);
}
app.Run();
static string Money(decimal amount) => amount.ToString("0.00", CultureInfo.InvariantCulture);
public record Login(string? Email, string? Password);
public static class DummyPassword { public static readonly string Hash = new PasswordHasher<AppUser>().HashPassword(new AppUser(), Guid.NewGuid().ToString()); }
public partial class Program { }
