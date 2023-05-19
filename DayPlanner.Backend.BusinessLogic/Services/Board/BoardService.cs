using AutoMapper;
using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class BoardService : IBoardService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public BoardService(DataContext context, 
            IMapper mapper,
            IUserContextService userContextService) 
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public async Task<int> CreateBoard(CreateBoardModel createBoardModel)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var creator = await _context.Users.FindAsync(currentUserId);
                

            var board = new Board
            {
                Name = createBoardModel.Name,
                CreatorId = currentUserId,
                Creator = creator,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _context.Boards.AddAsync(board);
            await _context.SaveChangesAsync();

            return board.Id;
        }

        public async Task DeleteBoard(int boardId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var board = await _context.Boards.FirstOrDefaultAsync(x => x.Id == boardId);

            if (board == null)
            {
                throw new ApplicationException("Board not found.");
            }

            if (board.CreatorId != currentUserId)
            {
                throw new ApplicationException("Access denied.");
            }

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }
    }
}
