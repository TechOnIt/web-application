using iot.Infrastructure.Common.Notifications.Contracts;
using iot.Infrastructure.Common.Notifications.Results;

namespace iot.Infrastructure.Common.Notifications.KaveNegarSms;

public interface IKaveNegarSmsService : IBaseNotifications
{
    Task<(SendStatus Status, string Message)> SendAsync(string to, string message);
    Task<(SendStatus Status,string Message)> SendAuthSmsAsync(string to, string apiKey, string template, string code);
}
