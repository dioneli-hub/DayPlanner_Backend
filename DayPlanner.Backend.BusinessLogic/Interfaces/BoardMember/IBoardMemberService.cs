

namespace DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember
{
    public interface IBoardMemberService
    {
        Task AddBoardMemberByEmail(int boardId, string userEmail);

        Task DeleteBoardMember(int boardId, int userId);

    }
}
