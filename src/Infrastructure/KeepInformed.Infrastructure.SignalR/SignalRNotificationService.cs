using KeepInformed.Common.MultiTenancy;
using KeepInformed.Common.Notifications;
using KeepInformed.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace KeepInformed.Infrastructure.SignalR;

public class SignalRNotificationService : INotificationService
{
    private readonly ITenantProvider _tenantProvider;
    private readonly IHubContext<NotificationHub> _hubContext;

    public SignalRNotificationService(ITenantProvider tenantProvider, IHubContext<NotificationHub> hubContext)
    {
        _tenantProvider = tenantProvider;
        _hubContext = hubContext;
    }

    public async Task NotifyAllUsers(string method, object? data = null)
    {
        var clients = _hubContext.Clients.All;

        await NotifyInternal(clients, method, data);
    }

    public async Task NotifyCurentUser(string method, object? data = null)
    {
        var userId = _tenantProvider.GetUserId().ToString();
        var client = _hubContext.Clients.User(userId);

        await NotifyInternal(client, method, data);
    }

    private async Task NotifyInternal(IClientProxy clientProxy, string method, object? data = null)
    {
        await clientProxy.SendAsync(method, data);
    }
}
