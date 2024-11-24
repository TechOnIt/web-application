using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Structures
{
    public class StructureDeletedEvent : BaseEvent
    {
        public StructureDeletedEvent(StructureEntity structure)
        {
            Structure = structure;
        }

        public StructureEntity Structure { get; set; }
    }
}