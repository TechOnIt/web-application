using TechOnIt.Domain.Entities.Catalog;

namespace TechOnIt.Domain.Events.Groups
{
    public class GroupDeletedEvent : BaseEvent
    {
        public GroupDeletedEvent(Group group)
        {
            Group = group;
        }

        public Group Group { get; set; }
    }
}