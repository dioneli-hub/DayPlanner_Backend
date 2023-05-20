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
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;
        private readonly ITaskItemProvider _taskItemProvider;
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemProvider taskItemProvider,
            ITaskItemService _taskItemService,
            IMapper mapper)
        {
            _taskItemProvider = taskItemProvider;
            _mapper = mapper;
        }

        [HttpGet (Name = nameof(GetTasks))]
        public async Task<ActionResult<List<TaskItemModel>>> GetTasks()
        {
            var tasks = await _taskItemProvider.GetTasks();
            return Ok(tasks);
        }


        //[HttpGet("{taskItemId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //public IActionResult GetTask(int taskItemId)
        //{
        //    if (!_taskItemRepository.TaskItemExists(taskItemId))
        //        return NotFound();

        //    var taskItem = _mapper.Map<TaskItemModel>(_taskItemRepository.GetTaskItem(taskItemId));

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(taskItem);
        //}


        [HttpGet("todaystasks", Name =nameof(GetTodaysTasks))] 
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

        //[HttpDelete("{taskId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult DeleteTask(int taskId)
        //{
        //    if (!_taskItemRepository.TaskItemExists(taskId))
        //    {
        //        return NotFound();
        //    }

        //    var taskToDelete = _taskItemRepository.GetTaskItem(taskId);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (!_taskItemRepository.DeleteTaskItem(taskToDelete))
        //    {
        //        ModelState.AddModelError("", "Something went wrong during deleting process...");
        //    }

        //    return NoContent();
        //}



    }
}