using System.Net.Mail;
using TechOnIt.Application.Common.Interfaces.Clients.Notifications;

namespace TechOnIt.Infrastructure.Common.Notifications.SmtpClientEmail;

public class SmtpEmailService : ISmtpEmailSender
{
    /// <summary>
    /// Send bulk email to multy addresses.
    /// </summary>
    /// <param name="from">From email address.</param>
    /// <param name="toCollection">Email addresses reciever.</param>
    /// <param name="subject">Email title.</param>
    /// <param name="htmlBody">Email body as html format.</param>
    /// <param name="settings">SMTP client setting.</param>
    public async Task<SendStatus> SendBulkEmailAsync(string from, string[] toCollection, string subject, string htmlBody, SmtpClient settings)
    {
        try
        {
            using var Client = settings;
            foreach (var to in toCollection)
            {
                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(to));
                    emailMessage.From = new MailAddress(from);
                    emailMessage.Subject = subject;
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = htmlBody;

                    Client.Send(emailMessage);

                    emailMessage.Dispose();
                };
            }

            Client.Dispose();
            return SendStatus.Sent;
        }
        catch
        {
            return SendStatus.Fail;
        }
    }

    /// <summary>
    /// Send an email to a specific address.
    /// </summary>
    /// <param name="from">From email address.</param>
    /// <param name="to">Email address reciever.</param>
    /// <param name="subject">Email title.</param>
    /// <param name="htmlBody">Email body as html format.</param>
    /// <param name="settings">SMTP client setting.</param>
    public async Task<SendStatus> SendEmailAsync(string from, string to, string subject, string htmlBody, SmtpClient settings)
    {
        try
        {
            using var Client = settings;

            using (var emailMessage = new MailMessage())
            {
                emailMessage.To.Add(new MailAddress(to));
                emailMessage.From = new MailAddress(from);
                emailMessage.Subject = subject;
                emailMessage.IsBodyHtml = true;
                emailMessage.Body = htmlBody;

                Client.Send(emailMessage);

                emailMessage.Dispose();
                Client.Dispose();
            };

            return SendStatus.Successeded;
        }
        catch
        {
            return SendStatus.Fail;
        }
    }
}