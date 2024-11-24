using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Structures
{
    public class StructureUpdatedEvent : BaseEvent
    {
        public StructureUpdatedEvent(StructureEntity structure)
        {
            Structure = structure;
        }

        public StructureEntity Structure { get; set; }
    }
}