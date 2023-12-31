﻿using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.TaskItem;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IBoardService
    {
        Task<int> CreateBoard(CreateBoardModel createBoardModel);
        Task DeleteBoard(int boardId);
        Task<int> AddTaskToBoard(int boardId, AddTaskItemToBoardModel addTaskItemToBoardModel);
        Task UpdateBoard(int boardId, EditBoardModel editBoardModel);
    }
}
