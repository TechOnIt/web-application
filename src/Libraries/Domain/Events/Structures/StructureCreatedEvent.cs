using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Structures
{
    public class StructureCreatedEvent : BaseEvent
    {
        public StructureCreatedEvent(StructureEntity structure)
        {
            Structure = structure;
        }

        public StructureEntity Structure { get; set; }
    }
}