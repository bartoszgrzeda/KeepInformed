namespace KeepInformed.Common.Notifications;

public interface INotificationService
{
    Task NotifyAllUsers(string method, object? data = null);
    Task NotifyCurentUser(string method, object? data = null);
}
