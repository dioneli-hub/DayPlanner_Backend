using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.BusinessLogic.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly DataContext _context;

        public TaskItemRepository(DataContext context)
        {
            _context = context;
        }

        ICollection<TaskItem> ITaskItemRepository.GetTasks()
        {
            return _context.TaskItems
                .Include(x => x.Board)
                .OrderBy(t => t.Id)
                .ToList();
        }

        public ICollection<TaskItem> GetTodaysTasks()
        {
            return _context.TaskItems
                .Include(x => x.Board)
                .Where (item => item.DueDate >= DateTime.Now.Date &&
                         item.DueDate <= DateTime.Now.AddDays(1))
                .OrderBy(t => t.Id).ToList();
        }

        public TaskItem GetTaskItem(int taskItemId)
        {
            return _context.TaskItems
                .Where(t => t.Id == taskItemId)
                .FirstOrDefault();
        }

        public bool TaskItemExists(int taskItemId)
        {
            return _context.TaskItems.Any(t => t.Id == taskItemId);
        }

        public bool UpdateTask(TaskItem task)
        {
            _context.Update(task);
            return Save();
        }
        public bool DeleteTaskItem(TaskItem task)
        {
            _context.Remove(task);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


        
    }
}
