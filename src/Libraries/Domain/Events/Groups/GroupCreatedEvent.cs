using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Groups
{
    public class GroupCreatedEvent : BaseEvent
    {
        public GroupCreatedEvent(GroupEntity group)
        {
            Group = group;
        }

        public GroupEntity Group { get; set; }
    }
}