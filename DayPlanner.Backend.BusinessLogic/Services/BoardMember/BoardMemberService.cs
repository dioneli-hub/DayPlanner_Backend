using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.BusinessLogic.Interfaces.Notification;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class BoardMemberService : IBoardMemberService
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        private readonly INotificationService _notificationService;
        public BoardMemberService(DataContext context,
            IUserContextService userContextService,
            INotificationService notificationService) 
        {
            _context = context;
            _userContextService = userContextService;
            _notificationService = notificationService;
        }
        public async Task<int> AddBoardMemberByEmail(int boardId, string userEmail)
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
            
            var currentUser = await _context.Users
                .Where(x => x.Id == currentUserId)
                .FirstOrDefaultAsync();

            var notificationModel = new CreateNotificationModel
            {
                Text = $"{currentUser?.FirstName} {currentUser?.LastName} added you to board \"{board.Name}\".",
                UserId = userToAdd.Id
            };
            await _notificationService.CreateNotification(notificationModel);

            return boardMember.MemberId;
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

            var notificationModel = new CreateNotificationModel
            {
                Text = $"You were deleted from board \"{board.Name}\".",
                UserId = userId
            };
            await _notificationService.CreateNotification(notificationModel);
        }

        public async Task LeaveBoard(int userId, int boardId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var board = await _context.Boards.FindAsync(boardId);

            if(userId != currentUserId)
            {
                throw new ApplicationException("Cannot leave as another user.");
            }

            if (board == null)
            {
                throw new ApplicationException("Board not found.");
            }

            var boardMembership = await _context.BoardMembers
                .Where(m => m.BoardId == boardId && m.MemberId == userId)
                .FirstOrDefaultAsync();

            if (boardMembership == null)
            {
                throw new ApplicationException("Current user is not a member of the board.");
            }

            _context.BoardMembers.Remove(boardMembership);
            await _context.SaveChangesAsync();

            var currentUser = await _context.Users
                                .Where(x=> x.Id == currentUserId)
                                .FirstOrDefaultAsync();

            var notificationModel = new CreateNotificationModel
            {
                Text = $"{currentUser?.FirstName} {currentUser?.LastName} left board \"{board.Name}\".",
                UserId = board.CreatorId
            };
            await _notificationService.CreateNotification(notificationModel);
        }
    }
}
