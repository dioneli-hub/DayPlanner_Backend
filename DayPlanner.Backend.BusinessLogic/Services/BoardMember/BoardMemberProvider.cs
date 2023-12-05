using AutoMapper;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.BoardMember;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class BoardMemberProvider : IBoardMemberProvider
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BoardMemberProvider(DataContext context, IMapper mapper) 
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

        public async Task<List<UserModel>> GetBoardMembers(int boardId)
        {
            var members = await _context.BoardMembers
                .Include(x => x.Member)
                .Where(m => m.BoardId == boardId)
                .Select(x=>x.Member)
                .ToListAsync();

            var memberModels = _mapper.Map<List<UserModel>>(members);

            return memberModels;
        }

        public async Task<List<string>> GetSuggestedSearchEmails(string emailSerached)
        {
            var emails =  await _context.Users
                .Where( x => x.Email.ToLower().Contains(emailSerached.ToLower()))
                .Select(x => x.Email)
                .ToListAsync();

           
            return emails;
        }


        /*
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
        */

        //public async Task<List<BoardModel>> GetMemberBoards(int userId) {

        //    var boards = await _context.BoardMembers
        //        .Include(x => x.Board.Creator)
        //        .Where(x => x.MemberId == userId)
        //        .Select(x => x.Board)

        //        .ToListAsync();

        //    var boardModels = _mapper.Map<List<BoardModel>>(boards);

        //    return boardModels;
        //}

        //public async Task<List<BoardModel>> GetUserBoards(int userId)
        //{
        //    var boards = await _context.Boards
        //        .Include(x => x.Creator)
        //        .Where(x => x.CreatorId == userId || x.BoardMembers.Any(u => u.Id == userId))
        //        .ToListAsync();

        //    var boardModels = _mapper.Map<List<BoardModel>>(boards);

        //    return boardModels;
        //}
        public async Task<List<BoardModel>> GetUserBoards(int userId)
        {
            var boards = await _context.BoardMembers
                .Where(x => x.MemberId == userId)
                .Select(x => x.Board)
                .ToListAsync();

            var boardModels = _mapper.Map<List<BoardModel>>(boards);

            return boardModels;
        }
    }
}
