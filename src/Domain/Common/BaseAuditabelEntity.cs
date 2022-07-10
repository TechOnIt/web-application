﻿using System;

namespace iot.Domain.Entities.Common
{
    public abstract class BaseAuditabelEntity 
    {
        public DateTime CreatedDateTime { get; set; }
        public Guid? CreatedBy { get; set; } // nullabel

        public DateTime? ModifyDateTime { get; set; } // nullabel
        public Guid? ModifyBy { get; set; } // nullabel
    }
}