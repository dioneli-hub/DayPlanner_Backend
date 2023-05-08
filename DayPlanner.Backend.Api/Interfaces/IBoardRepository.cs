﻿using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.Api.Interfaces
{
    public interface IBoardRepository
    {
        ICollection<Board> GetBoards();
        Board GetBoard(int boardId);
        bool BoardExists(int boardId);
        bool CreateBoard(Board board);
        bool DeleteBoard(Board board);
        bool UpdateBoard(Board board);
        bool AddTask(int boardId, TaskItem taskMap);
        bool RemoveTask(int boardId, int taskId);
        
    }
}
