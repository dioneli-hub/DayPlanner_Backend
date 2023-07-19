using AutoMapper;
using DayPlanner.Backend.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember;
using DayPlanner.Backend.ApiModels.BoardMember;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Services;

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
        [Route("boards/{boardId}/add-board-member-by-email/{userEmail}", Name = nameof(AddBoardMemberByEmail))]
        public async Task<ActionResult<UserModel>> AddBoardMemberByEmail(
            [FromRoute] int boardId,
            [FromRoute] string userEmail 
            )
        {
            var userId = await _boardMemberService.AddBoardMemberByEmail(boardId, userEmail);
            var user = await _userProvider.GetUser(userId);
            return Ok(user);
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
            [FromRoute] int boardId
            
            )
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


        /*
[HttpGet]
[Route("boards/{boardId}/memberships", Name = nameof(GetBoardMembers))]
public async Task<ActionResult<List<BoardMemberModel>>> GetBoardMembers(
    [FromRoute] int boardId)
{
    var boardMembers = await _boardMemberProvider.GetBoardMembers(boardId);
    return Ok(boardMembers);
}
*/


        //[HttpGet("users/{userId}/get-user-boards", Name = nameof(GetMemberBoards))]
        //public async Task<ActionResult<List<BoardModel>>> GetMemberBoards(int userId)
        //{
        //    var boards = await _boardMemberProvider.GetMemberBoards(userId);

        //    return Ok(boards);
        //}

    }
}

