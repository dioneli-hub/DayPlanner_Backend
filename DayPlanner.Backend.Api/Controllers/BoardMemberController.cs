using AutoMapper;
using DayPlanner.Backend.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember;
using DayPlanner.Backend.ApiModels.BoardMember;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoardMemberController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBoardMemberProvider _boardMemberProvider;
        private readonly IBoardMemberService _boardMemberService;

        public BoardMemberController(IMapper mapper, 
            IBoardMemberService boardMemberService, 
            IBoardMemberProvider boardMemberProvider)
        {
            _boardMemberProvider = boardMemberProvider;
            _boardMemberService = boardMemberService;
            _mapper = mapper;
        }

        [HttpGet("{userId}/member-boards", Name = nameof(GetMemberBoards))]
        public async Task<ActionResult<BoardModel>> GetMemberBoards(int userId)
        {
            var boards = await _boardMemberProvider.GetMemberBoards(userId);

            return Ok(boards);
        }

        [HttpGet]
        [Route("{boardId}/members", Name = nameof(GetBoardMembers))]
        public async Task<ActionResult<List<BoardMemberModel>>> GetBoardMembers(
            [FromRoute] int boardId)
        {
            var boardMembers = await _boardMemberProvider.GetBoardMembers(boardId);
            return Ok(boardMembers);
        }

        [HttpPost]
        [Route("{boardId}/add-board-member-by-email", Name = nameof(AddBoardMemberByEmail))]
        public async Task<ActionResult> AddBoardMemberByEmail(
            [FromRoute] int boardId,
            [FromBody] string userEmail 
            )
        {
            await _boardMemberService.AddBoardMemberByEmail(boardId, userEmail);
            return Ok();
        }

        //[HttpDelete]
        //[Route("{boardId}/members")]
        //public ActionResult DeleteBoardMember(
        //    [FromBody] int userId, //later will probably pass by email
        //    [FromRoute] int boardId)
        //{
        //    _boardMemberRepository.DeleteBoardMember(userId, boardId); //pass whole user as argument
        //    return Ok("Success");
        //}

    }
}

