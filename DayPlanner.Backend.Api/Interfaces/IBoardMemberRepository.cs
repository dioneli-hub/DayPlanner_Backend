using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.Api.Interfaces
{
    public interface IBoardMemberRepository
    {
        ICollection<User> GetBoardMembers(int boardId); //automapper
        ICollection<Board> GetUserMemberBoards(int userId); //automapper
        void MakeMember(int makeMemberUserId, int boardId);
        void DeleteMember(int deleteMemberUserId, int boardId);
    }
}
