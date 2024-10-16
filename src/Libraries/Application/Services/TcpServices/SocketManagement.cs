using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TechOnIt.Application.Services.TcpServices;

public class SocketManagement : IDisposable, ISocketManagement
{
    private bool disposedValue;
    public delegate void MessageReceivedEventHandler(object sender, object message);
    private Action<object> onMessageReceivedAction;
    private IPAddress _ipaddress;
    private int _port;

    /// <summary>
    /// spcefic ipaddress and port
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <param name="port"></param>
    public SocketManagement(string ipAddress, int port)
    {
        SetIPAddress(ipAddress);
        SetPort(port);
    }

    /// <summary>
    /// default ip address on with given port
    /// </summary>
    /// <param name="port"></param>
    public SocketManagement(int port)
    {
        _ipaddress = IPAddress.Any;
        SetPort(port);
    }

    /// <summary>
    /// default ip address and port 3000
    /// </summary>
    public SocketManagement()
    {
        _ipaddress = IPAddress.Any;
        _port = 3000;
    }


    #region events
    protected virtual void OnMessageReceived(object e) => onMessageReceivedAction?.Invoke(e);
    #endregion


    #region connection
    public Task StartConnection(Action<object> onMessageReceivedAction)
    {
        this.onMessageReceivedAction = onMessageReceivedAction;

        TcpClient client = new TcpClient(_ipaddress.ToString(), _port);
        NetworkStream stream = client.GetStream();

        while (true)
        {
            try
            {
                byte[] data = new byte[1024];
                int length = stream.Read(data, 0, data.Length);
                string message = Encoding.UTF8.GetString(data, 0, length);
                OnMessageReceived(message);
            }
            catch
            {
                continue;
            }
        }
    }
    public Task StartConnection()
    {
        TcpClient client = new TcpClient(_ipaddress.ToString(), _port);
        NetworkStream stream = client.GetStream();

        while (true)
        {
            try
            {
                byte[] data = new byte[1024];
                int length = stream.Read(data, 0, data.Length);
                string message = Encoding.UTF8.GetString(data, 0, length);
                OnMessageReceived(message);
            }
            catch
            {
                continue;
            }
        }
    }
    #endregion

    #region utilities
    private void SetIPAddress(string ipadddress)
    {
        if (string.IsNullOrWhiteSpace(ipadddress) == true)
            throw new ArgumentNullException(nameof(ipadddress));

        this._ipaddress = IPAddress.Parse(ipadddress);
    }
    private void SetPort(int port)
    {
        if (port == 0)
            throw new ArgumentOutOfRangeException(nameof(port));

        if (port < 1 && port > 6000)
            throw new ArgumentOutOfRangeException(nameof(port));

        if (port.ToString().Length < 3)
            throw new ArgumentOutOfRangeException(nameof(port));

        this._port = port;
    }
    #endregion

    #region disposPattern
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
