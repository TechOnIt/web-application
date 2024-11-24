using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Structures
{
    public class StructureDeletedEvent : BaseEvent
    {
        public StructureDeletedEvent(Structure structure)
        {
            Structure = structure;
        }

        public Structure Structure { get; set; }
    }
}