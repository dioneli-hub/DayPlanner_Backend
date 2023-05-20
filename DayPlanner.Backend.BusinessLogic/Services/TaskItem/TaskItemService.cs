using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.BusinessLogic.Repositories;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        public TaskItemService(DataContext context,
            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task DeleteTask(int taskId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var task = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
            {
                throw new ApplicationException("Task not found.");
            }

            if (task.CreatorId != currentUserId)
            {
                throw new ApplicationException("Access denied.");
            }

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
