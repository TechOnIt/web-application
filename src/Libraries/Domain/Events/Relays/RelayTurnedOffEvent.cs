using TechOnIt.Domain.Entities.Controllers;

namespace TechOnIt.Domain.Events.Relays
{
    public class RelayTurnedOffEvent
    {
        public RelayTurnedOffEvent(RelayEntity relay)
        {
            Relay = relay;
        }

        public RelayEntity Relay { get; set; }
    }
}