using TechOnIt.Domain.Entities.Controllers;

namespace TechOnIt.Domain.Events.Controllers.Relays
{
    public class RelayTurnedOnEvent
    {
        public RelayTurnedOnEvent(RelayEntity relay)
        {
            Relay = relay;
        }

        public RelayEntity Relay { get; set; }
    }
}