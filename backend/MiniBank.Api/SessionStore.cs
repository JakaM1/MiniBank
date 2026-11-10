using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MiniBank.Api;

// Vstopnica ostane v procesu; piškotek vsebuje samo zaščiten naključni ključ.
public sealed class SessionStore(TimeProvider clock) : ITicketStore
{
    private readonly ConcurrentDictionary<string, (AuthenticationTicket Ticket, DateTimeOffset LastSeen)> sessions = new();
    private static readonly TimeSpan IdleTimeout = TimeSpan.FromMinutes(30);
    public Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var now = clock.GetUtcNow();
        foreach (var entry in sessions)
            if (now - entry.Value.LastSeen >= IdleTimeout) sessions.TryRemove(entry.Key, out _);
        var key = Guid.NewGuid().ToString("N");
        sessions[key] = (ticket, now);
        return Task.FromResult(key);
    }
    public Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        if (sessions.TryGetValue(key, out var entry))
            sessions.TryUpdate(key, (ticket, clock.GetUtcNow()), entry);
        return Task.CompletedTask;
    }
    public Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        while (sessions.TryGetValue(key, out var entry))
        {
            var now = clock.GetUtcNow();
            if (now - entry.LastSeen >= IdleTimeout)
            {
                sessions.TryRemove(key, out _);
                return Task.FromResult<AuthenticationTicket?>(null);
            }
            if (sessions.TryUpdate(key, (entry.Ticket, now), entry))
                return Task.FromResult<AuthenticationTicket?>(entry.Ticket);
        }
        return Task.FromResult<AuthenticationTicket?>(null);
    }
    public Task RemoveAsync(string key) { sessions.TryRemove(key, out _); return Task.CompletedTask; }
}
