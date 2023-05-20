using AutoMapper;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.BoardMember;
using DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
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

        public async Task<BoardMemberModel> GetBoardMember(int boardId, int boardMemberId)
        {
            var boardMember = await _context.BoardMembers
                .Include(x => x.Board)
                .Include(x=>x.Member)
                .Where(m => m.BoardId == boardId && m.MemberId == boardMemberId)
                .FirstOrDefaultAsync();

            var boardMemberModel = _mapper.Map<BoardMemberModel>(boardMember);

            return boardMemberModel;
        }

        public async Task<List<BoardMemberModel>> GetBoardMembers(int boardId)
        {
            var boardMembers = await _context.BoardMembers
                .Include(x => x.Board)
                .Include(x => x.Member)
                .Where(m => m.BoardId == boardId)
                .ToListAsync();

            var boardMemberModels = _mapper.Map<List<BoardMemberModel>>(boardMembers);

            return boardMemberModels;
        }

        public async Task<List<BoardModel>> GetMemberBoards(int userId) {

            var boards = await _context.BoardMembers
                .Include(x => x.Board.Creator)
                .Where(x => x.MemberId == userId)
                .Select(x => x.Board)
                
                .ToListAsync();

            var boardModels = _mapper.Map<List<BoardModel>>(boards);

            return boardModels;
        }
    }
}
