using iot.Infrastructure.Common.Notifications.Results;
using System.Net.Mail;

namespace iot.Infrastructure.Common.Notifications.Contracts;

public interface ISendEmail : IBaseNotifications
{
    Task<SendStatus> SendEmailAsync(string from, string to, string subject, string htmlBody, SmtpClient settings);
    Task<SendStatus> SendBulkEmailAsync(string from, string[] toCollection, string subject, string htmlBody, SmtpClient settings);
}