using AutoMapper;
using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember;

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


    }
}

