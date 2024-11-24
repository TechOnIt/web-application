using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Domain.Events.Users
{
    public class UserDeletedEvent : BaseEvent
    {
        public UserDeletedEvent(UserEntity user)
        {
            User = user;
        }

        public UserEntity User { get; }
    }
}