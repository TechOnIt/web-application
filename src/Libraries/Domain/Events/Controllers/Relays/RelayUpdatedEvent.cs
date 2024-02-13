using TechOnIt.Domain.Entities.Controllers;

namespace TechOnIt.Domain.Events.Controllers.Relays
{
    public class RelayUpdatedEvent
    {
        public RelayUpdatedEvent(RelayEntity relay)
        {
            Relay = relay;
        }

        public RelayEntity Relay { get; set; }
    }
}