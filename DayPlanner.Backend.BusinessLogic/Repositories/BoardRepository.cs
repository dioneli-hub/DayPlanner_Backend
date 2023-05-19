using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.BusinessLogic.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public BoardRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }
        public ICollection<Board> GetBoards() //mapper
        {
            return _context.Boards
                .OrderBy(t => t.Id).ToList();
        }
        public Board GetBoard(int boardId)
        {
            return _context.Boards
                .Where(b => b.Id == boardId)
                .FirstOrDefault();
        }

        public bool BoardExists(int boardId)
        {
            return _context.Boards.Any(b => b.Id == boardId);
        }

        public bool CreateBoard(Board board)
        {
            // improve later
            board.CreatedAt = DateTime.Now;
            board.CreatorId = _userContextService.GetCurrentUserId(); 
            
            
            _context.Add(board);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteBoard(Board board)
        {
            _context.Remove(board);
            return Save();
        }

        public bool UpdateBoard(Board board)
        {
            _context.Update(board);
            return Save();
        }

        public bool AddTask(int boardId, TaskItem taskMap)
        {
            // update access so that only board members ??or task creator could create
            taskMap.CreatedAt = DateTime.Now;
            taskMap.Board = GetBoard(boardId);
            taskMap.CreatorId = _userContextService.GetCurrentUserId(); ;

            _context.TaskItems.Add(taskMap);
            return Save();
        }

        public bool RemoveTask(int boardId, int taskId)
        {
            var task = _context.TaskItems.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                throw new ApplicationException("Task not found.");
            }

            if (task.BoardId != boardId) // update access so that only board members ??or task creator could delete
            {
                throw new ApplicationException("Access denied");
            }

            _context.TaskItems.Remove(task);

            return Save();
        }

        public bool UpdateTask(TaskItem task)
        {
            _context.Update(task);
            return Save();
        }

        
    }
}
