using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.BoardMember;


namespace DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember
{
    public interface IBoardMemberProvider
    {
        Task<List<BoardModel>> GetUserBoards(int userId);
        Task<List<BoardModel>> GetMemberBoards(int userId);
        //Task<List<BoardMemberModel>> GetBoardMembers(int boardId);
        Task<List<UserModel>> GetBoardMembers(int boardId);
        Task<BoardMemberModel> GetBoardMember(int boardId, int boardMemberId);
    }
}
