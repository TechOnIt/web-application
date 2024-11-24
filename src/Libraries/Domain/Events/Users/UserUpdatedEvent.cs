using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Domain.Events.Users
{
    public class UserUpdatedEvent : BaseEvent
    {
        public UserUpdatedEvent(UserEntity user)
        {
            User = user;
        }

        public UserEntity User { get; }
    }
}