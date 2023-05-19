using DayPlanner.Backend.ApiModels.Board;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IBoardService
    {
        Task<int> CreateBoard(CreateBoardModel createBoardModel);
        Task DeleteBoard(int boardId);
    }
}
