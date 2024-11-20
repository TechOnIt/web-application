using TechOnIt.Domain.Common;

namespace TechOnIt.Application.Common.Interfaces.Clients.Notifications;

public class SendStatus : Enumeration
{
    public static readonly SendStatus Sent = new(1, nameof(Sent));
    public static readonly SendStatus Successeded = new(2, nameof(Successeded));
    public static readonly SendStatus Fail = new(3, nameof(Fail));
    public static readonly SendStatus BadRequest = new(4, nameof(BadRequest));

    public SendStatus() { }

    public SendStatus(int id, string name)
        : base(id, name) { }
}