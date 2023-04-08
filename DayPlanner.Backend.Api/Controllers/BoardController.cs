using AutoMapper;
using DayPlanner.Backend.Api.ApiModels;
using DayPlanner.Backend.Api.DTOs;
using DayPlanner.Backend.Api.Interfaces;
using DayPlanner.Backend.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : Controller
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public BoardController(IBoardRepository boardRepository, IMapper mapper)
        {
            _boardRepository = boardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Task>))]
        public IActionResult GetBoards()
        {
            var boards = _mapper.Map<List<BoardDto>>(_boardRepository.GetBoards());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(boards);
            }
        }

        [HttpGet("{boardId}")]
        public IActionResult GetTask(int boardId)
        {
            if (!_boardRepository.BoardExists(boardId))
                return NotFound();

            var board = _mapper.Map<BoardDto>(_boardRepository.GetBoard(boardId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(board);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBoard([FromBody] BoardDto boardCreate)
        {
            if (boardCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var boardMap = _mapper.Map<Board>(boardCreate);

            if (!_boardRepository.CreateBoard(boardMap))
            {
                ModelState.AddModelError("", "Something went wrong...");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{boardId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBoard(int boardId)
        {
            if (!_boardRepository.BoardExists(boardId))
            {
                return NotFound();
            }

            var boardToDelete = _boardRepository.GetBoard(boardId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_boardRepository.DeleteBoard(boardToDelete))
            {
                ModelState.AddModelError("", "Something went wrong during deleting process...");
            }

            return NoContent();
        }

        [HttpPut("{boardId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBoard(int boardId, 
            [FromBody] BoardDto updatedBoard)
        {
            if (updatedBoard == null)
                return BadRequest(ModelState);

            if (boardId != updatedBoard.Id)
                return BadRequest(ModelState);

            if (!_boardRepository.BoardExists(boardId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var boardMap = _mapper.Map<Board>(updatedBoard);

            if (!_boardRepository.UpdateBoard(boardMap))
            {
                ModelState.AddModelError("", "Something went wrong during updating...");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("{boardId}/tasks")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public ActionResult<TaskItem> AddTaskToBoard(
            [FromBody] AddTaskItemToBoardModel taskCreate,
            [FromRoute] int boardId)
        {
            if (taskCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskMap = _mapper.Map<TaskItem>(taskCreate);

            if (!_boardRepository.AddTask(boardId, taskMap))
            {
                ModelState.AddModelError("", "Something went wrong...");
                return StatusCode(500, ModelState);
            }
            //var task = _boardRepository.AddTask(boardId, taskCreate.Text, taskCreate.DueDate);
            //var task = _taskItemRepository.Get(taskId);
            return Ok("Successfully created");
        }

        [HttpDelete]
        [Route("{boardId}/tasks/{taskId}")]
        public ActionResult RemoveTaskFromBoard(
            [FromRoute] int boardId,
            [FromRoute] int taskId)
        {
            _boardRepository.RemoveTask(boardId, taskId);
            return Ok();
        }

       
    }
}
