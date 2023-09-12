
using DayPlanner.Backend.ApiModels.BoardMember;
using DayPlanner.Backend.BusinessLogic.ServiceResponse;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IBoardMemberService
    {
        Task<ServiceResponse<SmallBoardMemberModel>> AcceptInvitation(string invitationToken);
        Task<ServiceResponse<SmallBoardMemberModel>> DeclineInvitation(string invitationToken);
        Task<ServiceResponse<int>> InviteBoardMemberByEmail(int boardId, string userEmail);

        Task DeleteBoardMember(int boardId, int userId);
        Task LeaveBoard(int userId, int boardId);

    }
}
