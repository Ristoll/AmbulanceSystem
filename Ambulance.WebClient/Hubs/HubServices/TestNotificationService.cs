using Ambulance.WebAPI.Hubs;
using Ambulance.WebAPI.Hubs.HubServices;
using Microsoft.AspNetCore.SignalR;

public class NotificationService : BackgroundService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var userNotification = new Notification
                {
                    Type = NotificationTypes.Personal.ToString(),
                    Title = "Особисте повідомлення",
                    Message = "Це персоналізоване повідомлення для тебе (я за вікном)!",
                    Time = DateTime.UtcNow
                };

                var serviceNotification = new Notification
                {
                    Type = NotificationTypes.Service.ToString(),
                    Title = "Тестове повідомлення",
                    Message = "Це тестове повідомлення від NotificationService кожні 2 хвилини (я не придумав що)",
                    Time = DateTime.UtcNow
                };

                await _hubContext.Clients.User("14") // не забути User ID = PersonId
                    .SendAsync("ReceiveMessage", userNotification, cancellationToken: stoppingToken);

                await _hubContext.Clients.All
                    .SendAsync("ReceiveMessage", serviceNotification, cancellationToken: stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка в NotificationService: {ex.Message}");
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

    // для надсилання повідомлення конкретному користувачу
    public async Task SendToUser(int userId, string title, string message)
    {
        var notification = new Notification
        {
            Title = title,
            Message = message,
            Time = DateTime.UtcNow
        };

        await _hubContext.Clients.User(userId.ToString())
            .SendAsync("ReceiveMessage", notification);
    }
}
