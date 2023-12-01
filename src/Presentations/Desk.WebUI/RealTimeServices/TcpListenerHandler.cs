using Microsoft.AspNetCore.SignalR;
using System.Net.Sockets;
using System.Text;
using TechOnIt.Desk.WebUI.Hubs;
namespace TechOnIt.Desk.WebUI.RealTimeServices;

public class TcpListenerHandler : BackgroundService
{
    private readonly TcpClient _tcpListener;
    private readonly IHubContext<SensorHub> _hubContext;

    public TcpListenerHandler(IHubContext<SensorHub> hubContext)
    {
        _hubContext = hubContext;
        _tcpListener = new TcpClient("127.0.0.1", 3000);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(() => HandleDeviceAsync(), stoppingToken);
    }

    private async Task HandleDeviceAsync()
    {
        NetworkStream stream = _tcpListener.GetStream();
        while (true)
        {
            try
            {
                byte[] data = new byte[1024];
                int length = stream.Read(data, 0, data.Length);
                string message = Encoding.UTF8.GetString(data, 0, length);
                await _hubContext.Clients.All.SendAsync(message);
            }
            catch
            {
                continue;
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }
}