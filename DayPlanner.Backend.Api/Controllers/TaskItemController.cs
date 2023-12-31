﻿using DayPlanner.Backend.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.ApiModels.Recurrence;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Services;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskItemController : Controller
    {
        private readonly ITaskItemProvider _taskItemProvider;
        private readonly ITaskItemService _taskItemService;
        private readonly IRecurrenceService _recurrenceService;
        private readonly IRecurrenceProvider _recurrenceProvider;

        public TaskItemController(
            ITaskItemProvider taskItemProvider,
            ITaskItemService taskItemService,
            IRecurrenceService recurrenceService,
            IRecurrenceProvider recurrenceProvider
            )
        {
            _taskItemService = taskItemService;
            _taskItemProvider = taskItemProvider;
            _recurrenceService = recurrenceService;
            _recurrenceProvider = recurrenceProvider;
        }


        [HttpGet("{boardId}/get-board-tasks/if-my-{ifMyTasks}", Name = nameof(GetBoardTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetBoardTasks(int boardId, bool ifMyTasks)
        {
            var tasks = await _taskItemProvider.GetBoardTasks(boardId, ifMyTasks);

            return Ok(tasks);
        }

        [HttpGet("recurrence/{parentTaskId}/child-tasks", Name = nameof(GetChildTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetChildTasks([FromRoute] int parentTaskId)
        {
            var childTasks = await _recurrenceProvider.GetChildTasks(parentTaskId);
            return Ok(childTasks);
        }

        [HttpPatch("{taskId}", Name = nameof(UpdateTask))]
        public async Task<ActionResult<TaskItemModel>> UpdateTask(
          [FromRoute] int taskId,
          [FromBody] EditTaskItemModel editedTaskModel)
        {
            var updatedTaskId = await _taskItemService.UpdateTask(taskId, editedTaskModel);
            var updatedTask = await _taskItemProvider.GetTask(updatedTaskId);

            return Ok(updatedTask);
        }


        [HttpPatch("{taskId}/update-performer/{newPerformerId}", Name = nameof(UpdateTaskPerformer))]
        public async Task<ActionResult<TaskItemModel>> UpdateTaskPerformer(
         [FromRoute] int taskId, int newPerformerId)
        {
            await _taskItemService.UpdateTaskPerformer(taskId, newPerformerId);
            var updatedTask = await _taskItemProvider.GetTask(taskId);

            return Ok(updatedTask);
        }

        [HttpPatch("{taskId}/update-overdue", Name = nameof(UpdateTaskOverdue))]
        public async Task<ActionResult<TaskItemModel>> UpdateTaskOverdue(
                [FromRoute] int taskId)
        {
            await _taskItemService.UpdateTaskOverdue(taskId);
            var updatedTask = await _taskItemProvider.GetTask(taskId);

            return Ok(updatedTask);
        }


        [HttpDelete("{taskId}", Name = nameof(DeleteTask))]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            await _taskItemService.DeleteTask(taskId);
            return Ok(); //"Task successfully deleted."
        }

        [HttpGet("{userId}/user-boards-tasks", Name = nameof(GetUserBoardsTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetUserBoardsTasks(
                                                                    [FromRoute] int userId)
        {
            var usersTasks = await _taskItemProvider.GetUserBoardsTasks(userId);
            return Ok(usersTasks);
        }

        [HttpPost("{taskId}/complete-task", Name = nameof(CompleteTask))]
        public async Task<ActionResult> CompleteTask(
            [FromRoute] int taskId)
        {
            await _taskItemService.CompleteTask(taskId);
            return Ok("Task completed.");
        }

        [HttpPost("{taskId}/mark-task-as-todo", Name = nameof(MarkTaskAsToDo))]
        public async Task<ActionResult> MarkTaskAsToDo(
            [FromRoute] int taskId)
        {
            await _taskItemService.MarkTaskAsToDo(taskId);
            return Ok("Task marked as ToDo.");
        }

        [HttpPost("{taskId}/update-change-recurred-children", Name = nameof(UpdateChangeRecurredChildren))]
        public async Task<ActionResult<bool>> UpdateChangeRecurredChildren(
            [FromRoute] int taskId)
        {
            var updatedChangeRecurredChildren = await _taskItemService.UpdateChangeRecurredChildren(taskId);
            return Ok(updatedChangeRecurredChildren);
        }

        [HttpPost]
        [Route("add-recurrence")]
        public async Task<ActionResult<List<TaskItemModel>>> AddRecurrence([FromBody] RecurringPatternModel patternModel)
        {
            var patternId = await _recurrenceService.AddRecurrence(patternModel);
            var childTasks = await _recurrenceService.GenerateChildTasks(patternId);
            return Ok(childTasks);
        }

        [HttpGet("{boardId}/get-board-tasks/grouped-by-performer", Name = nameof(GetBoardTasksGroupedByPerformer))]
        public async Task<ActionResult<List<TaskGroup<UserModel>>>> GetBoardTasksGroupedByPerformer(int boardId)
        {
            var taskGroups = await _taskItemProvider.GetBoardTasksGroupedByPerformer(boardId);

            return Ok(taskGroups);
        }

        [HttpGet("{boardId}/get-board-tasks/grouped-by-completed/if-my-{ifMyTasks}", Name = nameof(GetBoardTasksGroupedByCompleted))]
        public async Task<ActionResult<List<TaskGroup<bool>>>> GetBoardTasksGroupedByCompleted(int boardId, bool ifMyTasks)
        {
            var taskGroups = await _taskItemProvider.GetBoardTasksGroupedByCompleted(boardId, ifMyTasks);

            return Ok(taskGroups);
        }
    }
}