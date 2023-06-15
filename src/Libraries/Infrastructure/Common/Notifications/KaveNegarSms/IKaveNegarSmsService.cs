using TechOnIt.Infrastructure.Common.Notifications.Contracts;
using TechOnIt.Infrastructure.Common.Notifications.Results;

namespace TechOnIt.Infrastructure.Common.Notifications.KaveNegarSms;

public interface IKaveNegarSmsService : IBaseNotifications
{
    Task<(SendStatus Status, string Message)> SendAsync(string to, string message);
    Task<(SendStatus Status, string Message)> SendAuthSmsAsync(string to, string apiKey, string template, string code);
}
