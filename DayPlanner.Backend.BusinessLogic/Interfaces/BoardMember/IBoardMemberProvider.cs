using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.BoardMember;


namespace DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember
{
    public interface IBoardMemberProvider
    {
        Task<List<BoardModel>> GetMemberBoards(int userId);
        Task<List<BoardMemberModel>> GetBoardMembers(int boardId);
        Task<BoardMemberModel> GetBoardMember(int boardId, int boardMemberId);
    }
}
