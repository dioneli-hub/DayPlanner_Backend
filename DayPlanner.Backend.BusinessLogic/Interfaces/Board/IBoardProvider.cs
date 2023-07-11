using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IBoardProvider
    {
        Task<List<BoardModel>> GetBoards();
        Task<BoardModel> GetBoard(int boardId);

        

    }
}
