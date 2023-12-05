using DayPlanner.Backend.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.ApiModels.BoardMember;
using DayPlanner.Backend.ApiModels.Auth;
using DayPlanner.Backend.BusinessLogic.ServiceResponse;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoardMemberController : Controller
    {
        private readonly IBoardMemberProvider _boardMemberProvider;
        private readonly IBoardMemberService _boardMemberService;
        private readonly IUserProvider _userProvider;

        public BoardMemberController(
            IBoardMemberService boardMemberService,
            IBoardMemberProvider boardMemberProvider,
            IUserProvider userProvider
            )
        {
            _userProvider = userProvider;
            _boardMemberProvider = boardMemberProvider;
            _boardMemberService = boardMemberService;
        }

        [HttpGet]
        [Route("boards/{boardId}/get-members", Name = nameof(GetBoardMembers))]
        public async Task<ActionResult<List<UserModel>>> GetBoardMembers(
            [FromRoute] int boardId)
        {
            var boardMembers = await _boardMemberProvider.GetBoardMembers(boardId);
            return Ok(boardMembers);
        }

        [HttpPost]
        [Route("boards/{boardId}/add-board-member-by-email/{userEmail}", Name = nameof(InviteBoardMemberByEmail))]
        public async Task<ActionResult<ServiceResponse<int>>> InviteBoardMemberByEmail(
            [FromRoute] int boardId,
            [FromRoute] string userEmail 
            )
        {
            var invitationIdResponse = await _boardMemberService.InviteBoardMemberByEmail(boardId, userEmail);

            return Ok(invitationIdResponse);

        }

        [HttpPatch("accept-invitation", Name = nameof(AcceptInvitation))]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<SmallBoardMemberModel>>> AcceptInvitation([FromBody] SmallTokenModel invitationToken)
        {
            var boardMemberResponse = await _boardMemberService.AcceptInvitation(invitationToken.Token);
            //var boardMemberResponse = await _boardMemberService.AcceptInvitation(WebUtility.UrlDecode(invitationToken.Token));

            return Ok(boardMemberResponse);
        }

        [HttpPatch("decline-invitation", Name = nameof(DeclineInvitation))]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<SmallBoardMemberModel>>> DeclineInvitation([FromBody] SmallTokenModel invitationToken)
        {
            var boardMemberResponse = await _boardMemberService.DeclineInvitation(invitationToken.Token);
            //var boardMemberResponse = await _boardMemberService.DeclineInvitation(WebUtility.UrlDecode(invitationToken.Token));

            return Ok(boardMemberResponse);
        }

        [HttpDelete]
        [Route("boards/{boardId}/delete-member/{userId}", Name = nameof(DeleteBoardMember))]
        public async Task<ActionResult> DeleteBoardMember(
            [FromRoute] int boardId,
            [FromRoute] int userId
            )
        {
            await _boardMemberService.DeleteBoardMember(boardId, userId); 
            return Ok(); //"Board member successfully deleted."
        }

        [HttpDelete]
        [Route("boards/{userId}/leave-board/{boardId}", Name = nameof(LeaveBoard))]
        public async Task<ActionResult> LeaveBoard(
            [FromRoute] int userId,
            [FromRoute] int boardId)
        {
            await _boardMemberService.LeaveBoard( userId, boardId);
            return Ok(); //"Board member successfully deleted."
        }


        [HttpGet("{userId}/user-boards", Name = nameof(GetUserBoards))]
        public async Task<ActionResult<List<BoardModel>>> GetUserBoards(int userId)
        {
            var userBoards = await _boardMemberProvider.GetUserBoards(userId);
            return Ok(userBoards);
        }


        [HttpGet("get-suggested-search-users/{emailSearched}", Name = nameof(GetSuggestedSearchEmails))]
        public async Task<ActionResult<List<string>>> GetSuggestedSearchEmails([FromRoute] string emailSearched)
        {

            var emails = await _boardMemberProvider.GetSuggestedSearchEmails(emailSearched);

            return Ok(emails);
        }
    }
}

