using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.BusinessLogic.Interfaces.Notification;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        private readonly INotificationService _notificationService;
        public TaskItemService(DataContext context,
            IUserContextService userContextService,
            INotificationService notificationService)
        {
            _context = context;
            _userContextService = userContextService;
            _notificationService = notificationService;
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

            //if (task.CreatorId != currentUserId)
            //{
            //    throw new ApplicationException("Access denied.");
            //}

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

        public async Task UpdateTaskPerformer(int taskId, int newPerformerId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var task = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == taskId);
            var newPerformer = await _context.Users.FirstOrDefaultAsync(x => x.Id == newPerformerId);

            if (newPerformer == null)
            {
                throw new ApplicationException("No data to update entered.");
            }

            if (task == null)
            {
                throw new ApplicationException("Task not found.");
            }

            if (task.CreatorId != currentUserId)
            {
                throw new ApplicationException("Access denied: only task creator can edit the task.");
            }


            var board = await _context.Boards
                .Include(x => x.Creator)
                .Where(x => x.Id == task.BoardId)
                .FirstOrDefaultAsync();

            var boardHasCurrentMemberNullFlag = await _context.BoardMembers
                .AnyAsync(x => x.MemberId == currentUserId && x.BoardId == board.Id);

            //if (task.Creator.Id != currentUserId && boardHasCurrentMemberNullFlag)
            //{
            //    throw new ApplicationException("Access denied: cannot move task to the board if the user is neither its owner nor its member.");
            //}

            task.PerformerId = newPerformerId;
            task.Performer = newPerformer;

            _context.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskOverdue(int taskId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var task = await _context.TaskItems
                .Include(x=> x.Board)
                .FirstOrDefaultAsync(x => x.Id == taskId);
           

            if (task == null)
            {
                throw new ApplicationException("Task not found.");
            }

            var newOverdue = (task.DueDate.CompareTo(DateTimeOffset.UtcNow.Date) < 0)
                && task.IsCompleted == false ?
                            true : false;

            if(task.IsOverdue == false && newOverdue == true && (currentUserId == task.PerformerId || currentUserId == task.CreatorId))
            {
                var notificationModel = new CreateNotificationModel
                {
                    Text = $"Your task \"{task.Text}\" from board \"{task.Board.Name}\" was spotted overdue."
                };

                await _notificationService.CreateNotification(notificationModel);

            }
            task.IsOverdue = newOverdue;

            _context.Update(task);
            await _context.SaveChangesAsync();
        }


        public async Task AssignTaskPerformer(int taskId, int performerId)
        {
            var task = await _context.TaskItems
                .Where(x => x.Id == taskId)
                .FirstOrDefaultAsync();

            var isPerformerBoardMember = await _context.BoardMembers
                                            .AnyAsync(x => x.BoardId == task.BoardId && x.MemberId == performerId);

            if (task != null && isPerformerBoardMember)
            {
                task.PerformerId = performerId;
                task.Performer = await _context.Users
                    .Where(x => x.Id == performerId)
                    .FirstOrDefaultAsync();

                _context.Update(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveTaskPerformer(int taskId)
        {
            var task = await _context.TaskItems
                .Where(x => x.Id == taskId)
                .FirstOrDefaultAsync();

            if (task != null)
            {
                task.PerformerId = null;
                task.Performer = null;

                _context.Update(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
