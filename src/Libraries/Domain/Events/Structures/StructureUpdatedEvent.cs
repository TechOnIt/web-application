﻿using TechOnIt.Domain.Entities.Catalog;

namespace TechOnIt.Domain.Events.Structures
{
    public class StructureUpdatedEvent : BaseEvent
    {
        public StructureUpdatedEvent(Structure structure)
        {
            Structure = structure;
        }

        public Structure Structure { get; set; }
    }
}