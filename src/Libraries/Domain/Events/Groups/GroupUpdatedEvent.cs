using TechOnIt.Domain.Entities.Catalog;

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