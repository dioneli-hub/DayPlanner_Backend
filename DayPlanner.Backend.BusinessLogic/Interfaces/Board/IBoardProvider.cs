using DayPlanner.Backend.ApiModels.Board;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IBoardProvider
    {
        Task<List<BoardModel>> GetBoards();
        Task<BoardModel> GetBoard(int boardId);
    }
}
