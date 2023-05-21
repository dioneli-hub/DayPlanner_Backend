using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
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

        public async Task CompleteTask(int taskId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var task = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
            {
                throw new ApplicationException("Task not found.");
            }

            if (task.CreatorId != currentUserId && task.PerformerId != currentUserId)
            {
                throw new ApplicationException("Access denied: only task creator or task performer can complete task.");
            }

            task.IsCompleted = true;

            _context.Update(task);
            await _context.SaveChangesAsync();
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

        public async Task MarkTaskAsToDo(int taskId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var task = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
            {
                throw new ApplicationException("Task not found.");
            }

            if (task.CreatorId != currentUserId && task.PerformerId != currentUserId)
            {
                throw new ApplicationException("Access denied: only task creator or task performer can complete task.");
            }

            task.IsCompleted = false;

            _context.Update(task);
            await _context.SaveChangesAsync();
        }
    

        public async Task UpdateTask(int taskId, EditTaskItemModel editedTaskModel)
        {
            if (editedTaskModel == null)
            {
                throw new ApplicationException("No data to update entered.");
            }

            var currentUserId = _userContextService.GetCurrentUserId();
            var task = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == taskId);

            

            if (task == null)
            {
                throw new ApplicationException("Task not found.");
            }

            if (task.CreatorId != currentUserId)
            {
                throw new ApplicationException("Access denied: only task creator can edit the task.");
            }

            if (editedTaskModel.BoardId != task.BoardId)
            {
                var boardFromModel = await _context.Boards
                    .Include(x => x.Creator)
                    .Where(x => x.Id == editedTaskModel.BoardId)
                    .FirstOrDefaultAsync();

                var boardHasCurrentMemberNullFlag = await _context.BoardMembers
                    .AnyAsync(x => x.MemberId == currentUserId && x.BoardId == boardFromModel.Id);

                if (boardFromModel.Creator.Id != currentUserId && boardHasCurrentMemberNullFlag)
                {
                    throw new ApplicationException("Access denied: cannot move task to the board if the user is neither its owner nor its member.");
                }
            }

            task.Text = editedTaskModel.Text;
            task.DueDate = editedTaskModel.DueDate;
            task.BoardId = editedTaskModel.BoardId;

            _context.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
