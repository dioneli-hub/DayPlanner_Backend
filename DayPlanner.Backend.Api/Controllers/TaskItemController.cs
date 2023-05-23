﻿using DayPlanner.Backend.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.ApiModels.TaskItem;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskItemController : Controller
    {
        private readonly ITaskItemProvider _taskItemProvider;
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(
            ITaskItemProvider taskItemProvider,
            ITaskItemService taskItemService
            )
        {
            _taskItemService = taskItemService;
            _taskItemProvider = taskItemProvider;
        }

        [HttpGet(Name = nameof(GetTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetTasks()
        {
            var tasks = await _taskItemProvider.GetTasks();
            return Ok(tasks);
        }


        [HttpGet("{taskItemId}", Name = nameof(GetTask))]
        public async Task<ActionResult<TaskItemModel>> GetTask(int taskId)
        {
            var task = await _taskItemProvider.GetTask(taskId);

            return Ok(task);
        }


        [HttpGet("todaystasks", Name = nameof(GetTodaysTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetTodaysTasks()
        {
            var todaysTasks = await _taskItemProvider.GetTodaysTasks();
            return Ok(todaysTasks);
        }

        [HttpPut("{taskId}" , Name =nameof(UpdateTask))]
        public async Task<ActionResult<TaskItemModel>> UpdateTask(
           [FromRoute] int taskId,
           [FromBody] EditTaskItemModel editedTaskModel)
        {
            await _taskItemService.UpdateTask(taskId, editedTaskModel);
            var updatedTask = await _taskItemProvider.GetTask(taskId);

            return Ok(updatedTask);
        }



        [HttpDelete("{taskId}", Name =nameof(DeleteTask))]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            await _taskItemService.DeleteTask(taskId);
            return Ok("Task successfully deleted.");
        }


        [HttpGet("{userId}/users-completed-tasks", Name = nameof(GetUsersCompletedTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetUsersCompletedTasks(
            [FromRoute] int userId)
        {
            var usersCompletedTasks = await _taskItemProvider.GetUsersCompletedTasks(userId);
            return Ok(usersCompletedTasks);
        }

        [HttpGet("{userId}/users-tasks", Name = nameof(GetUsersTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetUsersTasks(
            [FromRoute] int userId)
        {
            var usersTasks = await _taskItemProvider.GetUsersTasks(userId);
            return Ok(usersTasks);
        }

        [HttpGet("{userId}/users-todo-tasks", Name = nameof(GetUsersToDoTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetUsersToDoTasks(
            [FromRoute] int userId)
        {
            var usersToDoTasks = await _taskItemProvider.GetUsersToDoTasks(userId);
            return Ok(usersToDoTasks);
        }

        [HttpGet("{userId}/users-todays-tasks", Name = nameof(GetUsersTodaysTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetUsersTodaysTasks(
            [FromRoute] int userId)
        {
            var usersTodaysTasks = await _taskItemProvider.GetUsersTodaysTasks(userId);
            return Ok(usersTodaysTasks);
        }

        [HttpPost("{taskId}/complete-task", Name = nameof(CompleteTask))]
        public async Task<ActionResult<List<TaskItemModel>>> CompleteTask(
            [FromRoute] int taskId)
        {
            await _taskItemService.CompleteTask(taskId);
            return Ok("Task completed.");
        }

        [HttpPost("{taskId}/mark-task-as-todo", Name = nameof(MarkTaskAsToDo))]
        public async Task<ActionResult<List<TaskItemModel>>> MarkTaskAsToDo(
            [FromRoute] int taskId)
        {
            await _taskItemService.MarkTaskAsToDo(taskId);
            return Ok("Task marked as ToDo.");
        }
    }
}