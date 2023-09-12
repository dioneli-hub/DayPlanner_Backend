using AutoMapper;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.BoardMember;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.ServiceResponse;
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
        private readonly IEmailService _emailService;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;
        public BoardMemberService(DataContext context,
            IUserContextService userContextService,
            INotificationService notificationService,
            IEmailService emailService,
            IHashService hashService,
            IMapper mapper) 
        {
            _context = context;
            _userContextService = userContextService;
            _notificationService = notificationService;
            _emailService = emailService;
            _hashService = hashService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<int>> InviteBoardMemberByEmail(int boardId, string userEmail)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            
            var board = await _context.Boards.FindAsync(boardId);

            if (board == null)
            {
                return new ServiceResponse<int>
                {
                    IsSuccess = false,
                    Message = "Board not found.",
                    Data = 0
                };
            }

            if (board.CreatorId != currentUserId)
            {
                return new ServiceResponse<int>
                {
                    IsSuccess = false,
                    Message = "Access denied: only board owner can add new board members.",
                    Data = 0
                };
            }

            var hasAnyMemberByEmail = await _context.BoardMembers.Where(x => (x.Member.Email == userEmail) && (x.BoardId == boardId)).AnyAsync();
            if (hasAnyMemberByEmail)
            {
                return new ServiceResponse<int>
                {
                    IsSuccess = false,
                    Message = "This user is already a member of this board.",
                    Data = 0
                };
            }

            var invitation = new BoardMembershipInvitation
            {
                InviterId = currentUserId,
                InvitedPersonEmail = userEmail,
                BoardId = boardId,
                CreatedAt = DateTimeOffset.UtcNow,
                InvitationToken = _hashService.GenerateRandomToken(64)
            };

            await _context.BoardMembershipInvitations.AddAsync(invitation);
            await _context.SaveChangesAsync();

            try
            {
                await _emailService.SendInviteToBoardEmail(currentUserId, userEmail, boardId);
                return new ServiceResponse<int>
                {
                    IsSuccess = true,
                    Message = "Invitation successfully sent to the user's mailbox.",
                    Data = invitation.Id
                };
            } catch
            {
                return new ServiceResponse<int>
                {
                    IsSuccess = false,
                    Message = "Invitation has not been sent. Please, check email entered for correctness!",
                    Data = 0
                };
            }

        }

        
        public async Task<ServiceResponse<SmallBoardMemberModel>> AcceptInvitation(string invitationToken)
        {

            var invitation = await _context.BoardMembershipInvitations
                    .Where(x => x.InvitationToken == invitationToken)
                    .FirstOrDefaultAsync();

            if (invitation == null)
            {
                return new ServiceResponse<SmallBoardMemberModel>
                {
                    IsSuccess = false,
                    Message = "Invitation is not valid.",
                    Data = null
                };
            }

            if(invitation.IsDeclinedAt != null)
            {
                return new ServiceResponse<SmallBoardMemberModel>
                {
                    IsSuccess = false,
                    Message = "Invitation has already been declined.",
                    Data = null
                };
            }

            if(invitation.IsAcceptedAt != null)
            {
                return new ServiceResponse<SmallBoardMemberModel>
                {
                    IsSuccess = false,
                    Message = "This invitation has already been accepted.",
                    Data = null
                };
            }

            var invitedUser = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == invitation.InvitedPersonEmail);

            if (invitedUser == null)
            {
                return new ServiceResponse<SmallBoardMemberModel>
                {
                    IsSuccess = false,
                    Message = "User has to be registered to join board.",
                    Data = null
                };
            }

            var board = await _context.Boards
                .FirstOrDefaultAsync(x => x.Id == invitation.BoardId);

            if (board == null)
            {
                return new ServiceResponse<SmallBoardMemberModel>
                {
                    IsSuccess = false,
                    Message = "Board no longer exists.",
                    Data = null
                };
            }


            var boardMember = new BoardMember
            {
                BoardId = board.Id,
                Board = board,
                MemberId = invitedUser.Id,
                Member = invitedUser
            };

            invitation.IsAcceptedAt = DateTimeOffset.UtcNow;
            _context.BoardMembershipInvitations.Update(invitation);

            await _context.BoardMembers.AddAsync(boardMember);
            await _context.SaveChangesAsync();

            var notificationModel = new CreateNotificationModel
            {
                Text = $"{invitedUser?.FirstName} {invitedUser?.LastName} accepted your invitation to join board \"{board.Name}\".",
                UserId = invitation.InviterId
            };

            await _notificationService.CreateNotification(notificationModel);

            return new ServiceResponse<SmallBoardMemberModel>
            {
                IsSuccess = true,
                Message = $"You were successfully added to board \"{board.Name}\".",
                Data = _mapper.Map<SmallBoardMemberModel>(boardMember)
            };
        }



        public async Task<ServiceResponse<SmallBoardMemberModel>> DeclineInvitation(string invitationToken)
        {

            var invitation = await _context.BoardMembershipInvitations
                    .Where(x => x.InvitationToken == invitationToken)
                    .FirstOrDefaultAsync();

            if (invitation == null)
            {
                return new ServiceResponse<SmallBoardMemberModel>
                {
                    IsSuccess = false,
                    Message = "Invitation is not valid.",
                    Data = null
                };
            }

            if (invitation.IsDeclinedAt != null)
            {
                return new ServiceResponse<SmallBoardMemberModel>
                {
                    IsSuccess = false,
                    Message = "Invitation has already been declined.",
                    Data = null
                };
            }

            if (invitation.IsAcceptedAt != null)
            {
                return new ServiceResponse<SmallBoardMemberModel>
                {
                    IsSuccess = false,
                    Message = "This invitation has already been accepted.",
                    Data = null
                };
            }

            var board = await _context.Boards
                   .FirstOrDefaultAsync(x => x.Id == invitation.BoardId);

            if (board == null)
            {   
                return new ServiceResponse<SmallBoardMemberModel>
                {
                    IsSuccess = false,
                    Message = "Board no longer exists.",
                    Data = null
                };
            }

            invitation.IsDeclinedAt = DateTimeOffset.UtcNow;

            _context.BoardMembershipInvitations.Update(invitation);
            await _context.SaveChangesAsync();

            var notificationModel = new CreateNotificationModel
            {
                Text = $"{invitation.InvitedPersonEmail} declined your invitation to join board \"{board.Name}\".",
                UserId = invitation.InviterId
            };

            await _notificationService.CreateNotification(notificationModel);

            return new ServiceResponse<SmallBoardMemberModel>
            {
                IsSuccess = true,
                Message = $"Invitation to board \"{board.Name}\" declined.",
                Data = null
            };
        }

        public async Task DeleteBoardMember(int boardId, int userId)
        {
            try
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
            catch
            {
                throw new ApplicationException("Some error has occured while deleting the board member.");
            }
            
        }

        public async Task LeaveBoard(int userId, int boardId)
        {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var board = await _context.Boards.FindAsync(boardId);

                if (userId != currentUserId)
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
                                    .Where(x => x.Id == currentUserId)
                                    .FirstOrDefaultAsync();

                var notificationModel = new CreateNotificationModel
                {
                    Text = $"{currentUser?.FirstName} {currentUser?.LastName} left board \"{board.Name}\".",
                    UserId = board.CreatorId
                };
                await _notificationService.CreateNotification(notificationModel);
            } catch
            {
                throw new ApplicationException("Some error has occured while attemting to leave the board.");
            }
           
        }
    }
}
