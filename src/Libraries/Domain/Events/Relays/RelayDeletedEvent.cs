using TechOnIt.Domain.Entities.Controllers;

namespace TechOnIt.Domain.Events.Relays
{
    public class RelayDeletedEvent
    {
        public RelayDeletedEvent(RelayEntity relay)
        {
            Relay = relay;
        }

        public RelayEntity Relay { get; set; }
    }
}