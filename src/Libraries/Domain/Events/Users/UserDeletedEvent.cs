using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Domain.Events.Users
{
    public class UserDeletedEvent : BaseEvent
    {
        public UserDeletedEvent(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}