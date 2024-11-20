using System.Net.Mail;

namespace TechOnIt.Application.Common.Interfaces.Clients.Notifications;

public interface IEmailSender : IBaseNotifications
{
    Task<SendStatus> SendEmailAsync(string from, string to, string subject, string htmlBody, SmtpClient settings);
    Task<SendStatus> SendBulkEmailAsync(string from, string[] toCollection, string subject, string htmlBody, SmtpClient settings);
}