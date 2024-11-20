﻿using TechOnIt.Domain.Entities.Board;

namespace TechOnIt.Domain.Events.Boards
{
    public class BoardDeletedEvent : BaseEvent
    {
        public BoardDeletedEvent(BoardEntity board)
        {
            Board = board;
        }

        public BoardEntity Board { get; set; }
    }
}