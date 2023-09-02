
using DayPlanner.Backend.ApiModels.BoardMember;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember
{
    public interface IBoardMemberService
    {
        Task<ServiceResponse<BoardMemberModel>> AcceptInvitation(string invitationToken);
        Task<ServiceResponse<BoardMemberModel>> DeclineInvitation(string invitationToken);
        Task<ServiceResponse<int>> InviteBoardMemberByEmail(int boardId, string userEmail);

        Task DeleteBoardMember(int boardId, int userId);
        Task LeaveBoard(int userId, int boardId);

    }
}
