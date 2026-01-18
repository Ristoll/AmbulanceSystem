using Microsoft.AspNetCore.SignalR;

namespace Ambulance.WebAPI.Hubs;

public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var personId = Context.UserIdentifier; // автоматично встановлюється з Person ID з нашого токену
        Console.WriteLine($"Користувач {personId} підключився. ConnectionId: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine($"Користувач {Context.UserIdentifier} відключився");
        await base.OnDisconnectedAsync(exception);
    }
}