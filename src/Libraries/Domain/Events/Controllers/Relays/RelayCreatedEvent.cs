using TechOnIt.Domain.Entities.Controllers;

namespace TechOnIt.Domain.Events.Controllers.Relays
{
    public class RelayCreatedEvent
    {
        public RelayCreatedEvent(RelayEntity relay)
        {
            Relay = relay;
        }

        public RelayEntity Relay { get; set; }
    }
}