﻿using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Services;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface ITaskItemProvider
    {
        Task<TaskItemModel> GetTask(int taskId);
        Task<List<TaskItemModel>> GetTasks();
        Task<List<TaskItemModel>> GetTodaysTasks();
        Task<List<TaskItemModel>> GetUsersTasks(int userId);
        Task<List<TaskItemModel>> GetUsersTodaysTasks(int userId);

        Task<List<TaskItemModel>> GetUserBoardsTasks(int userId);
        Task<List<TaskItemModel>> GetUserBoardsTodaysTasks(int userId);

        Task<List<TaskItemModel>> GetUsersCompletedTasks(int userId);
        Task<List<TaskItemModel>> GetUsersToDoTasks(int userId);

        Task<List<TaskItemModel>> GetBoardTasks(int boardId, bool ifMyTasks);
        Task<List<TaskGroup<UserModel>>> GetBoardTasksGroupedByPerformer(int boardId);
        Task<List<TaskGroup<bool>>> GetBoardTasksGroupedByCompleted(int boardId, bool ifMyTasks);
    }
}
