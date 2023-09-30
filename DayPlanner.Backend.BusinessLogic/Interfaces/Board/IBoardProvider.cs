using DayPlanner.Backend.ApiModels;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IBoardProvider
    {
        Task<List<BoardModel>> GetBoards();
        Task<BoardModel> GetBoard(int boardId);

        Task<bool> IsUserAllowedToBoard(int userId, int boardId);

    }
}
