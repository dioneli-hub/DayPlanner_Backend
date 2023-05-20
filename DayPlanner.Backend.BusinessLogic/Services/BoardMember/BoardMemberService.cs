using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class BoardMemberService : IBoardMemberService
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        public BoardMemberService(DataContext context,
            IUserContextService userContextService) 
        {
            _context = context;
            _userContextService = userContextService;
        }
        public async Task AddBoardMemberByEmail(int boardId, string userEmail)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var board = await _context.Boards.FindAsync(boardId);

            if (board == null)
            {
                throw new ApplicationException("Board not found.");
            }

            if (board.CreatorId != currentUserId)
            {
                throw new ApplicationException("Access denied: only board owner can add new board members.");
            }

            var userToAdd = await _context.Users
                .Where(u => u.Email == userEmail)
                .FirstOrDefaultAsync();

            if (userToAdd == null)
            {
                throw new ApplicationException("Error: the potential board member with such email not found.");
            }

            var boardMember = new BoardMember
            {
                BoardId = boardId,
                Board = board,
                MemberId = userToAdd.Id,
                Member = userToAdd
            };

            await _context.BoardMembers.AddAsync(boardMember);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBoardMember(int boardId, int userId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var board = await _context.Boards.FindAsync(boardId);

            if (board == null)
            {
                throw new ApplicationException("Board not found.");
            }

            if (board.CreatorId != currentUserId)
            {
                throw new ApplicationException("Access denied: only board owner can delete board members.");
            }

            var boardMember = await _context.BoardMembers
                .Where(m => m.BoardId == boardId && m.MemberId == userId)
                .FirstOrDefaultAsync();

            _context.BoardMembers.Remove(boardMember);
            await _context.SaveChangesAsync();
        }
    }
}
