using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MiniBank.Api;
using Npgsql;
using Xunit;

namespace MiniBank.Tests;

public sealed class TestClock : TimeProvider
{
    private DateTimeOffset now = DateTimeOffset.UtcNow;
    public override DateTimeOffset GetUtcNow() => now;
    public void Advance(TimeSpan time) => now += time;
}
public sealed class BankFactory : WebApplicationFactory<Program>
{
    private readonly SqliteConnection connection = new("Data Source=:memory:");
    private readonly string? postgres = Environment.GetEnvironmentVariable("MINIBANK_TEST_POSTGRES");
    private readonly string database = "minibank_tests_" + Guid.NewGuid().ToString("N");
    public Guid AnaId { get; } = Guid.NewGuid();
    public Guid AccountId { get; } = Guid.NewGuid();
    public Guid OtherAccountId { get; } = Guid.NewGuid();
    public TestClock Clock { get; } = new();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        string? testConnection = null;
        if (postgres is null) connection.Open();
        else
        {
            using var admin = new NpgsqlConnection(postgres); admin.Open();
            using var command = new NpgsqlCommand($"CREATE DATABASE \"{database}\"", admin); command.ExecuteNonQuery();
            testConnection = new NpgsqlConnectionStringBuilder(postgres) { Database = database }.ConnectionString;
        }
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<BankDb>>();
            services.RemoveAll<IDbContextOptionsConfiguration<BankDb>>();
            services.AddDbContext<BankDb>(o => { if (testConnection is null) o.UseSqlite(connection); else o.UseNpgsql(testConnection); });
            services.RemoveAll<TimeProvider>();
            services.AddSingleton<TimeProvider>(Clock);
            services.PostConfigure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, o => o.TimeProvider = Clock);
        });
    }
    public HttpClient Start()
    {
        var client = CreateClient(new() { AllowAutoRedirect = false });
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BankDb>();
        if (postgres is null) db.Database.EnsureCreated(); else db.Database.Migrate();
        var hasher = new PasswordHasher<AppUser>();
        foreach (var (id, name, role, active) in new[] { (AnaId, "ana", "CLIENT", true), (Guid.NewGuid(), "inactive", "CLIENT", false), (Guid.NewGuid(), "empty", "CLIENT", true), (Guid.NewGuid(), "admin", "ADMIN", true), (Guid.NewGuid(), "employee", "EMPLOYEE", true) })
        {
            var user = new AppUser { Id = id, Name = name, Email = $"{name}@minibank.test", Role = role, Active = active };
            user.PasswordHash = hasher.HashPassword(user, "Local-Test-Password"); db.Users.Add(user);
        }
        db.Accounts.AddRange(new BankAccount { Id = AccountId, OwnerId = AnaId, Number = "MB-ANA", Label = "Osebni", Balance = 200 }, new BankAccount { Id = OtherAccountId, OwnerId = db.Users.Local.Single(x => x.Name == "empty").Id, Number = "MB-BOR", Label = "Drug", Balance = 500 });
        for (var i = 0; i < 25; i++) db.Transfers.Add(new Transfer { SourceAccountId = AccountId, TargetAccountId = OtherAccountId, Amount = i + 1, Purpose = $"Test {i}", ExecutedAt = DateTime.UtcNow.AddMinutes(-i) });
        db.Transfers.Add(new Transfer { SourceAccountId = AccountId, TargetAccountId = OtherAccountId, Amount = 650, Status = "PENDING_APPROVAL", Purpose = "Čakajoče" });
        db.SaveChanges();
        return client;
    }
    public async Task<HttpResponseMessage> Login(HttpClient client, string name = "ana", string password = "Local-Test-Password")
    {
        await Csrf(client);
        var result = await client.PostAsJsonAsync("/api/auth/login", new { email = $"{name}@minibank.test", password });
        if (result.IsSuccessStatusCode) await Csrf(client);
        return result;
    }
    public static async Task Csrf(HttpClient client)
    {
        var token = await client.GetFromJsonAsync<JsonElement>("/api/auth/csrf");
        client.DefaultRequestHeaders.Remove("X-CSRF-TOKEN");
        client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", token.GetProperty("token").GetString());
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!disposing) return;
        connection.Dispose();
        if (postgres is not null)
        {
            NpgsqlConnection.ClearAllPools();
            using var admin = new NpgsqlConnection(postgres); admin.Open();
            using var command = new NpgsqlCommand($"DROP DATABASE IF EXISTS \"{database}\" WITH (FORCE)", admin); command.ExecuteNonQuery();
        }
    }
}
public class AcceptanceTests
{
    [Fact] public async Task US18_Login_logout_and_csrf()
    {
        using var app = new BankFactory(); using var client = app.Start();
        Assert.Equal(HttpStatusCode.Unauthorized, (await client.GetAsync("/api/accounts")).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, (await client.PostAsJsonAsync("/api/auth/login", new { email = "ana@minibank.test", password = "Local-Test-Password" })).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await app.Login(client)).StatusCode);
        Assert.Equal("CLIENT", (await client.GetFromJsonAsync<JsonElement>("/api/auth/me")).GetProperty("role").GetString());
        client.DefaultRequestHeaders.Remove("X-CSRF-TOKEN");
        Assert.Equal(HttpStatusCode.BadRequest, (await client.PostAsJsonAsync("/api/auth/logout", new { })).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/auth/me")).StatusCode);
        await BankFactory.Csrf(client);
        Assert.Equal(HttpStatusCode.NoContent, (await client.PostAsJsonAsync("/api/auth/logout", new { })).StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, (await client.GetAsync("/api/auth/me")).StatusCode);
    }
    [Fact] public async Task US18_Invalid_login_has_identical_response()
    {
        using var app = new BankFactory(); using var client = app.Start();
        string? expected = null;
        foreach (var name in new[] { "ana", "missing", "inactive" })
        {
            var response = await app.Login(client, name, name == "ana" ? "wrong" : "Local-Test-Password");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var body = await response.Content.ReadAsStringAsync(); expected ??= body; Assert.Equal(expected, body);
            Assert.Contains("Prijava ni uspela", (await response.Content.ReadFromJsonAsync<JsonElement>()).GetProperty("message").GetString());
        }
    }
    [Theory] [InlineData("admin")] [InlineData("employee")]
    public async Task US18_Other_roles_cannot_open_banking(string role)
    {
        using var app = new BankFactory(); using var client = app.Start();
        Assert.Equal(HttpStatusCode.OK, (await app.Login(client, role)).StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, (await client.GetAsync("/api/accounts")).StatusCode);
    }
    [Fact] public async Task US18_Deactivation_and_role_changes_revoke_open_session()
    {
        using var app = new BankFactory(); using var client = app.Start(); await app.Login(client);
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<BankDb>();
            var user = await db.Users.FindAsync(app.AnaId); user!.Active = false; await db.SaveChangesAsync();
        }
        Assert.Equal(HttpStatusCode.Unauthorized, (await client.GetAsync("/api/auth/me")).StatusCode);
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<BankDb>();
            var user = await db.Users.FindAsync(app.AnaId); user!.Active = true; await db.SaveChangesAsync();
        }
        Assert.Equal(HttpStatusCode.Unauthorized, (await client.GetAsync("/api/auth/me")).StatusCode);
        await app.Login(client);
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<BankDb>();
            var user = await db.Users.FindAsync(app.AnaId); user!.Role = "EMPLOYEE"; await db.SaveChangesAsync();
        }
        Assert.Equal(HttpStatusCode.Unauthorized, (await client.GetAsync("/api/accounts")).StatusCode);
    }
    [Fact] public async Task US18_Idle_timeout_and_restart_require_login()
    {
        using var app = new BankFactory(); using var client = app.Start(); await app.Login(client);
        app.Clock.Advance(TimeSpan.FromMinutes(20));
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/auth/me")).StatusCode);
        app.Clock.Advance(TimeSpan.FromMinutes(20));
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/auth/me")).StatusCode);
        app.Clock.Advance(TimeSpan.FromMinutes(30));
        Assert.Equal(HttpStatusCode.Unauthorized, (await client.GetAsync("/api/auth/me")).StatusCode);
        var freshStore = new SessionStore(TimeProvider.System);
        var store = app.Services.GetRequiredService<SessionStore>();
        var key = await store.StoreAsync(new AuthenticationTicket(new System.Security.Claims.ClaimsPrincipal(), "Cookies"));
        Assert.Null(await freshStore.RetrieveAsync(key));
    }
    [Fact] public async Task US01_Own_accounts_and_last_20_executed_transfers_only()
    {
        using var app = new BankFactory(); using var client = app.Start(); await app.Login(client);
        var accounts = await client.GetFromJsonAsync<JsonElement>("/api/accounts");
        Assert.Equal(1, accounts.GetArrayLength()); Assert.Equal("200.00", accounts[0].GetProperty("balance").GetString());
        Assert.False(accounts[0].TryGetProperty("ownerId", out _));
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync($"/api/accounts/{app.OtherAccountId}/transactions")).StatusCode);
        var history = await client.GetFromJsonAsync<JsonElement>($"/api/accounts/{app.AccountId}/transactions");
        Assert.Equal(20, history.GetArrayLength()); Assert.Equal("Test 0", history[0].GetProperty("purpose").GetString());
        Assert.Equal("Test 19", history[19].GetProperty("purpose").GetString());
        Assert.All(history.EnumerateArray(), t => Assert.Equal("OUT", t.GetProperty("direction").GetString()));
    }
    [Fact] public async Task US01_No_accounts_returns_empty_list()
    {
        using var app = new BankFactory(); using var client = app.Start();
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<BankDb>();
            var user = new AppUser { Name = "none", Email = "none@minibank.test" };
            user.PasswordHash = new PasswordHasher<AppUser>().HashPassword(user, "Local-Test-Password"); db.Users.Add(user); await db.SaveChangesAsync();
        }
        await app.Login(client, "none");
        Assert.Equal("[]", await client.GetStringAsync("/api/accounts"));
    }
}
