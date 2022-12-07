using System.Net.Mail;
using TechOnIt.Infrastructure.Common.Notifications.Results;

namespace TechOnIt.Infrastructure.Common.Notifications.Contracts;

public interface ISendEmail : IBaseNotifications
{
    Task<SendStatus> SendEmailAsync(string from, string to, string subject, string htmlBody, SmtpClient settings);
    Task<SendStatus> SendBulkEmailAsync(string from, string[] toCollection, string subject, string htmlBody, SmtpClient settings);
}