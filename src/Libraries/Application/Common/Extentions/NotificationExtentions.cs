using System.Net;
using System.Net.Mail;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Application.Services.AssemblyServices;

namespace TechOnIt.Application.Common.Extentions;

public static class NotificationExtentions
{
    public static string GetGmailSenderAddress(this IAppSettingsService<AppSettingDto> appSettings)
        => appSettings.Value.EmailSettings.Email;

    public static SmtpClient GetEmailSettings(this IAppSettingsService<AppSettingDto> appSettings, bool enabledSll = true)
    {
        using var Client = new SmtpClient();
        var Credential = new NetworkCredential
        {
            UserName = appSettings.Value.EmailSettings.Username,
            Password = appSettings.Value.EmailSettings.Password,
        };

        Client.Credentials = Credential;
        Client.Host = appSettings.Value.EmailSettings.Host;
        Client.Port = appSettings.Value.EmailSettings.Port;
        Client.EnableSsl = enabledSll;

        return Client;
    }
}