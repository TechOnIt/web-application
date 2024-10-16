using Microsoft.AspNetCore.SignalR;

namespace TechOnIt.Desk.Web.Hubs;

public class SensorHub : Hub
{
    public async Task SendReportToClients(string report)
    {
        await Clients.All.SendAsync("ReceiveReport", report);
    }
}
