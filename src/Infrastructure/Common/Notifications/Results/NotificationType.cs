using iot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Infrastructure.Common.Notifications.Results;

public class NotificationType : Enumeration
{
    public static readonly NotificationType KaveNegarSms = new(1, nameof(KaveNegarSms));
    public static readonly NotificationType SmtpEmail = new(2, nameof(SmtpEmail));

    public NotificationType() { }

    public NotificationType(int id, string name)
        : base(id, name) { }
}
