using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Groups
{
    public class GroupDeletedEvent : BaseEvent
    {
        public GroupDeletedEvent(GroupEntity group)
        {
            Group = group;
        }

        public GroupEntity Group { get; set; }
    }
}