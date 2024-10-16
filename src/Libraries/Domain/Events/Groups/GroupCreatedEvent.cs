using TechOnIt.Domain.Entities.Catalog;

namespace TechOnIt.Domain.Events.Groups
{
    public class GroupCreatedEvent : BaseEvent
    {
        public GroupCreatedEvent(Group group)
        {
            Group = group;
        }

        public Group Group { get; set; }
    }
}