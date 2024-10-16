using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Domain.Events.Users
{
    public class UserCreatedEvent : BaseEvent
    {
        public UserCreatedEvent(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}