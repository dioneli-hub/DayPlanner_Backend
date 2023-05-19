using AutoMapper;
using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class BoardMemberProvider : IBoardMemberProvider
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        //private readonly IUserContextService _userContextService;
        public BoardMemberProvider(DataContext context, IMapper mapper) //IUserContextService userContextService
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BoardModel>> GetMemberBoards(int userId) {

            var boards = await _context.BoardMembers
                .Where(x => x.MemberId == userId)
                .Select(x => x.Board).ToListAsync();

            var boardModels = _mapper.Map<List<BoardModel>>(boards);

            return boardModels;
        }
    }
}
