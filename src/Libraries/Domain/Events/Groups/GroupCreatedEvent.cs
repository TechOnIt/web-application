using TechOnIt.Domain.Entities.Catalogs;

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