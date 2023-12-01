namespace TechOnIt.Application.Services.TcpServices;

public interface ISocketManagement
{
    Task StartConnection(Action<object> onMessageReceivedAction);
}