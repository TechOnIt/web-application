using TechOnIt.Domain.Entities.Board;

namespace TechOnIt.Domain.Events.Boards
{
    public class BoardUpdatedEvent : BaseEvent
    {
        public BoardUpdatedEvent(BoardEntity board)
        {
            Board = board;
        }

        public BoardEntity Board { get; set; }
    }
}