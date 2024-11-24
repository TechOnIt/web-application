using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Structures
{
    public class StructureCreatedEvent : BaseEvent
    {
        public StructureCreatedEvent(Structure structure)
        {
            Structure = structure;
        }

        public Structure Structure { get; set; }
    }
}