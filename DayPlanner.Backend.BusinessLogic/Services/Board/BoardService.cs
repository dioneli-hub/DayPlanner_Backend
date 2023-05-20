using AutoMapper;
using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.ApiModels.TaskItem;
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
        private readonly IUserContextService _userContextService;
        public BoardService(DataContext context, 
            IUserContextService userContextService) 
        {
            _context = context;
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

        public async Task<int> AddTaskToBoard(int boardId, AddTaskItemToBoardModel addTaskItemToBoardModel)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var currentUser = await _context.Users
                .Where(u => u.Id == currentUserId)
                .FirstOrDefaultAsync();
            var board = await _context.Boards.FindAsync(boardId);

            var boardHasCurrentMember = await _context.BoardMembers.AnyAsync(x => x.BoardId == boardId && x.MemberId == currentUserId);

            if (board == null)
            {
                throw new ApplicationException("Error: board with the given ID does not exist.");
            }

            if (board.CreatorId != currentUserId && !boardHasCurrentMember)
            {
                throw new ApplicationException("Access denied: only board creator and board members can add new tasks.");
            }

            var task = new TaskItem
            {
                Text = addTaskItemToBoardModel.Text,
                DueDate = addTaskItemToBoardModel.DueDate,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatorId = currentUserId,
                Creator = currentUser,
                BoardId = boardId,
                Board = board
            };

            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();

            return task.Id;
        }
    }
}
