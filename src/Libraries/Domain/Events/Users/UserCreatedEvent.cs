using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Domain.Events.Users
{
    public class UserCreatedEvent : BaseEvent
    {
        public UserCreatedEvent(UserEntity user)
        {
            User = user;
        }

        public UserEntity User { get; }
    }
}