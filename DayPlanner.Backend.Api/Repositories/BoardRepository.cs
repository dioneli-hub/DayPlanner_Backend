using DayPlanner.Backend.Api.Interfaces;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.Api.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly DataContext _context;

        public BoardRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Board> GetBoards()
        {
            return _context.Boards
                .OrderBy(t => t.Id).ToList();
        }
        public Board GetBoard(int boardId)
        {
            return _context.Boards.Where(b => b.Id == boardId).FirstOrDefault();
        }

        public bool BoardExists(int boardId)
        {
            return _context.Boards.Any(b => b.Id == boardId);
        }

        public bool CreateBoard(int currentUserId, Board board)
        {
            // improve later
            board.CreatedAt = DateTime.Now;
            board.CreatorId = currentUserId;
            
            
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

        public bool AddTask(int currentUserId, int boardId, TaskItem taskMap)
        {
            
            taskMap.BoardId = boardId;
            taskMap.CreatedAt = DateTime.Now;
            taskMap.Board = GetBoard(boardId);
            taskMap.CreatorId = currentUserId;

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

            if (task.BoardId != boardId)
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
