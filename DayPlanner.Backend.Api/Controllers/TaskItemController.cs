using AutoMapper;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.Domain;
using DayPlanner.Backend.ApiModels.TaskItem;
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
            var task = await _taskItemProvider.GetTask(taskItemId);

            return Ok(task);
        }


        [HttpGet("todaystasks", Name = nameof(GetTodaysTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetTodaysTasks()
        {
            var todaysTasks = await _taskItemProvider.GetTodaysTasks();
            return Ok(todaysTasks);
        }

        //[HttpPut("{taskId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult UpdateTask(
        //   [FromRoute] int taskId,
        //   [FromBody] EditTaskItemModel updatedTask)
        //{
        //    if (updatedTask == null)
        //        return BadRequest(ModelState);

        //    if (taskId != updatedTask.Id)
        //        return BadRequest(ModelState); 

        //    if (!_taskItemRepository.TaskItemExists(taskId))
        //        return NotFound();

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var taskMap = _mapper.Map<TaskItem>(updatedTask);


        //    if (!_taskItemRepository.UpdateTask(taskMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong during updating...");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();
        //}

        [HttpDelete("{taskId}", Name =nameof(DeleteTask))]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            await _taskItemService.DeleteTask(taskId);
            return Ok("Task successfully deleted.");
        }
    }
}