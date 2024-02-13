using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Domain.Events.Identities.Users
{
    public class UserUpdatedEvent : BaseEvent
    {
        public UserUpdatedEvent(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}