namespace TechOnIt.Application.Common.Interfaces.Clients.Notifications;

public interface ISmsSender : IBaseNotifications
{
    Task<(SendStatus Status, string Message)> SendAsync(string to, string message);
    Task<(SendStatus Status, string Message)> SendAuthSmsAsync(string to, string apiKey, string template, string code);
}