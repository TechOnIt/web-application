using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Groups
{
    public class GroupUpdatedEvent : BaseEvent
    {
        public GroupUpdatedEvent(Group group)
        {
            Group = group;
        }

        public Group Group { get; set; }
    }
}