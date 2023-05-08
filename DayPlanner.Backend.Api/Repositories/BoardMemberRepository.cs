using DayPlanner.Backend.Api.Interfaces;
using DayPlanner.Backend.Api.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.Api.Repositories
{
    public class BoardMemberRepository : IBoardMemberRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public BoardMemberRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        

        public ICollection<User> GetBoardMembers(int boardId)
        {
            var query = _context.BoardMembers
                .Where(x => x.BoardId == boardId)
                .Select(x => x.Member);
            return query.ToList();
        }

        public ICollection<Board> GetUserMemberBoards(int userId)
        {
            var query = _context.BoardMembers
                .Where(x => x.MemberId == userId)
                .Select(x => x.Board);
            return query.ToList();
        }

        public void MakeMember(int makeMemberUserId, int boardId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var board = _context.Boards
                .FirstOrDefault(x => x.Id == boardId);

            if (board?.CreatorId != currentUserId)
            {
                throw new ApplicationException("Cannot add member as not the board creator.");
            }

            var hasMembership = _context.BoardMembers
                .Any(x => x.MemberId == makeMemberUserId &&
                               x.BoardId == boardId);
            if (!hasMembership)
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == makeMemberUserId);

                if (user == null || board == null)
                {
                    throw new ApplicationException("User or board not found.");
                }

                var membership = new BoardMember
                {
                    MemberId = makeMemberUserId,
                    BoardId = boardId
                };
                _context.BoardMembers.Add(membership);
                _context.SaveChanges();
            }
        }

        public void DeleteMember(int deleteMemberUserId, int boardId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var board = _context.Boards
                .FirstOrDefault(x => x.Id == boardId);

            if (board?.CreatorId != currentUserId && currentUserId != deleteMemberUserId)
            {
                throw new ApplicationException("The current user is not authorized to cancel membership.");
            }

            var membership = _context.BoardMembers
                .FirstOrDefault(x => x.MemberId == deleteMemberUserId &&
                                          x.BoardId == boardId);

            if (membership != null)
            {
                _context.BoardMembers.Remove(membership);
                _context.SaveChanges();
            }
        }
    }
}
