using System;

namespace iot.Domain.Entities.Common
{
    public abstract class BaseAuditabelEntity 
    {
        public DateTime CreatedDateTime { get; set; }
        public Guid? CreatedBy { get; set; } // null abel

        public DateTime? ModifyDateTime { get; set; } // null abel
        public Guid? ModifyBy { get; set; } // null abel
    }
}
