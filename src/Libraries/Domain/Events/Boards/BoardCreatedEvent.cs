﻿using TechOnIt.Domain.Entities.Board;

namespace TechOnIt.Domain.Events.Boards
{
    public class BoardCreatedEvent : BaseEvent
    {
        public BoardCreatedEvent(BoardEntity board)
        {
            Board = board;
        }

        public BoardEntity Board { get; set; }
    }
}