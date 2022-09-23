using iot.Infrastructure.Common.Notifications.Results;
using System.Net.Mail;

namespace iot.Infrastructure.Common.Notifications.SmtpClientEmail;

public class SmtpEmailService : ISmtpEmailService
{
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
