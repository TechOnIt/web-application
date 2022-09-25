using iot.Infrastructure.Common.Notifications.Results;
using System.Text.Json;

namespace iot.Infrastructure.Common.Notifications.KaveNegarSms;

public class KaveNegarSmsService : IKaveNegarSmsService
{
    public async Task<(SendStatus Status, string Message)> SendAuthSmsAsync(string to, string apiKey, string template, string code)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync($"https://api.kavenegar.com/v1/{apiKey}/verify/lookup.json?receptor={to}&token={code}&template={template}");
            var contents = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<KavenegarResult>(contents);
            if (result is null)
                return (SendStatus.BadRequest, String.Empty);

            return GetStatusAndMessageResult(result.Return);
        }
        catch (Exception exp)
        {
            return (SendStatus.Fail, exp.Message);
        }
    }

    #region extentions
    private (SendStatus Status, string Message) GetStatusAndMessageResult(Return result)
    {
        switch (result.status)
        {
            case 401:
                return (SendStatus.Fail, "account is not active");
            case 403:
                return (SendStatus.Fail, "account is not valid - check api key");
            case 400:
                return (SendStatus.Fail, "Parameters are incomplete");
            case 402:
                return (SendStatus.Fail, "The operation was unsuccessful");
            case 404:
                return (SendStatus.Fail, "No method found with this name");
            case 405:
                return (SendStatus.Fail, "Calling the Get or Post method is wrong");
            case 409:
                return (SendStatus.Fail, "The server is unable to respond, try again later");
            case 414:
                return (SendStatus.Fail, "The request volume is greater than the limit");
            case 200:
                return (SendStatus.Successeded, "message sent successfully");
            default:
                return (SendStatus.Fail, "request faileds !");
        }
    }
    #endregion

    #region records
    private record KavenegarResult(Return Return, Entries entries);
    private record Return(int status, string message);
    private record Entries(DateTime datetime, int day, int hour, int minute, int month, int second, object unixtime, int year);
    #endregion
}
