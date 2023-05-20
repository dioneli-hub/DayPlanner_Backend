using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.Domain;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IBoardService
    {
        Task<int> CreateBoard(CreateBoardModel createBoardModel);
        Task DeleteBoard(int boardId);
        Task<int> AddTaskToBoard(int boardId, AddTaskItemToBoardModel addTaskItemToBoardModel);
    }
}
