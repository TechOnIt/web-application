using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Groups
{
    public class GroupUpdatedEvent : BaseEvent
    {
        public GroupUpdatedEvent(GroupEntity group)
        {
            Group = group;
        }

        public GroupEntity Group { get; set; }
    }
}