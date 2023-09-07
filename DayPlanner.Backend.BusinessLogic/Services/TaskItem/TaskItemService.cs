using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;

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
            try 
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
            } catch
            {
                throw new ApplicationException("Some error has occured while trying to complete task.");
            }
            
        }

        public async Task DeleteTask(int taskId)
        {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var task = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == taskId);

                if (task == null)
                {
                    throw new ApplicationException("Task not found.");
                }

                if (task.CreatorId != currentUserId)
                {
                    throw new ApplicationException("Access denied. Only task creator can delete the task.");
                }

                if (task.ChangeRecurredChildren == true)
                {

                    try
                    {
                        var childTasks = await _context.TaskItems
                                             .Where(x => x.ParentTaskId == task.Id)
                                             .ToListAsync();

                        _context.TaskItems.RemoveRange(childTasks);
                    }
                    catch
                    {
                        throw new ApplicationException("Something went wrong during deleting child tasks...");
                    };
                }

                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new ApplicationException("Some error has occured while trying to delete task.");
            }
        }

        public async Task MarkTaskAsToDo(int taskId)
        {
            try
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
            catch
            {
                throw new ApplicationException("Some error has occured while trying to mark task as To Do.");
            }
            
        }

        public async Task<bool> UpdateChangeRecurredChildren(int taskId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var task = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
            {
                throw new ApplicationException("Task not found.");
            }

            if (task.CreatorId != currentUserId)
            {
                throw new ApplicationException("Access denied: only task creator can update this information.");
            }

            task.ChangeRecurredChildren = !task.ChangeRecurredChildren;

            _context.Update(task);
            await _context.SaveChangesAsync();
            return task.ChangeRecurredChildren;
        }



        public async Task<int> UpdateTask(int taskId, EditTaskItemModel editedTaskModel)
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


            if (task.ChangeRecurredChildren == true && task.DueDate != editedTaskModel.DueDate)
            {

                var timeDifference = editedTaskModel.DueDate - task.DueDate;
                try
                {
                    var childTasks = await _context.TaskItems
                                         .Where(x => x.ParentTaskId == task.Id)
                                         .ToListAsync();

                    foreach (var childTask in childTasks)
                    {
                        childTask.DueDate = childTask.DueDate + timeDifference;
                        _context.Update(childTask);
                    }
                }
                catch
                {
                    throw new ApplicationException("Something went wrong during rescheduling child tasks...");
                };
            }

            task.Text = editedTaskModel.Text;
            task.DueDate = editedTaskModel.DueDate;
            task.BoardId = editedTaskModel.BoardId;

            _context.Update(task);
            await _context.SaveChangesAsync();

            return task.Id;
        }

        public async Task UpdateTaskPerformer(int taskId, int newPerformerId)
        {
            try
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


                task.PerformerId = newPerformerId;
                task.Performer = newPerformer;

                _context.TaskItems.Update(task);
                await _context.SaveChangesAsync();

                var notificationModel = new CreateNotificationModel
                {
                    Text = $"You were assigned performer of task \"{task.Text}\" at board \"{task.Board.Name}\".",
                    UserId = newPerformerId
                };
                await _notificationService.CreateNotification(notificationModel);
            }
            catch
            {
                throw new ApplicationException("Some error has occured while trying to update task performer.");
            }
        }

        public async Task UpdateTaskOverdue(int taskId)
        {
            try
            {

                var currentUserId = _userContextService.GetCurrentUserId();
                var task = await _context.TaskItems
                    .Include(x => x.Board)
                    .FirstOrDefaultAsync(x => x.Id == taskId);


                if (task == null)
                {
                    throw new ApplicationException("Task not found.");
                }

                var newOverdue = (task.DueDate.CompareTo(DateTimeOffset.UtcNow.Date) < 0)
                    && task.IsCompleted == false ?
                                true : false;

                if (task.IsOverdue == false && newOverdue == true && (currentUserId == task.PerformerId || currentUserId == task.CreatorId))
                {
                    var notificationModel = new CreateNotificationModel
                    {
                        Text = $"Your task \"{task.Text}\" from board \"{task.Board.Name}\" was spotted overdue.",
                        UserId = currentUserId
                    };

                    await _notificationService.CreateNotification(notificationModel);

                }
                task.IsOverdue = newOverdue;

                _context.Update(task);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new ApplicationException("Some error has occured while trying to update task overdue.");
            }
        }


        public async Task AssignTaskPerformer(int taskId, int performerId)
        {
            try
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

                    var notificationModel = new CreateNotificationModel
                    {
                        Text = $"You were assigned performer of task \"{task.Text}\" at board \"{task.Board.Name}\".",
                        UserId = performerId
                    };
                    await _notificationService.CreateNotification(notificationModel);
                } else
                {
                    throw new ApplicationException("Error assigning task a performer. Please, make sure the user being assigned is a member of the board.");
                }
            } catch 
            {
                throw new ApplicationException("Some error has occured while trying to assign task performer.");
            }
            
        }

        public async Task RemoveTaskPerformer(int taskId)
        {
            try
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
                } else
                {
                    throw new ApplicationException("Task to remove performer to not found.");
                }
            }
            catch
            {
                throw new ApplicationException("Some error has occured while trying to remove task performer.");
            }
        }
    }
}
