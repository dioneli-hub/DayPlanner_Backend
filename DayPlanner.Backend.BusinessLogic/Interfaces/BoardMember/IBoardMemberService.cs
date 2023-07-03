

namespace DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember
{
    public interface IBoardMemberService
    {
        Task<int> AddBoardMemberByEmail(int boardId, string userEmail);

        Task DeleteBoardMember(int boardId, int userId);
        Task LeaveBoard(int userId, int boardId);

    }
}
