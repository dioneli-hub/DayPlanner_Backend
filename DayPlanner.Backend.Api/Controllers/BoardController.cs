using AutoMapper;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.Domain;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.ApiModels;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoardController : Controller
    {
        private readonly IBoardProvider _boardProvider;
        private readonly IBoardService _boardService;
        private readonly IMapper _mapper;
        private readonly ITaskItemProvider _taskItemProvider;

        public BoardController(IMapper mapper,
            IBoardProvider boardProvider, 
            IBoardService boardService,
            ITaskItemProvider taskItemProvider)
        {
            _mapper = mapper;
            _boardProvider = boardProvider;
            _boardService = boardService;
            _taskItemProvider = taskItemProvider;
            
        }

        [HttpGet(Name = nameof(GetBoards))]
        public async Task<ActionResult<List<BoardModel>>> GetBoards()
        {
            var boards = await _boardProvider.GetBoards();

            return Ok(boards);
        }

        [HttpGet("{boardId}", Name = nameof(GetBoard))]
        public async Task<ActionResult<BoardModel>> GetBoard(int boardId)
        {
            var board = await _boardProvider.GetBoard(boardId);
            
            return Ok(board);
        }

        [HttpPost (Name = nameof(CreateBoard))]
        public async Task<ActionResult<BoardModel>> CreateBoard([FromBody] CreateBoardModel createBoardModel)
        {
            var boardId = await _boardService.CreateBoard(createBoardModel);
            var board = await _boardProvider.GetBoard(boardId);

            return Ok(board);
        }

        [HttpDelete("{boardId}", Name = nameof(DeleteBoard))]
        
        public async Task<ActionResult> DeleteBoard(int boardId)
        {
            await _boardService.DeleteBoard(boardId);

            return Ok();
        }

        //[HttpPut("{boardId}", Name = nameof(UpdateBoard))]

        //public IActionResult UpdateBoard(int boardId,
        //    [FromBody] BoardModel updatedBoard)
        //{
        //    if (updatedBoard == null)
        //        return BadRequest(ModelState);

        //    if (boardId != updatedBoard.Id)
        //        return BadRequest(ModelState);

        //    if (!_boardRepository.BoardExists(boardId))
        //        return NotFound();

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var boardMap = _mapper.Map<Board>(updatedBoard);

        //    if (!_boardRepository.UpdateBoard(boardMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong during updating...");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();
        //}

        [HttpPost]
        [Route("{boardId}/tasks")]
        public async Task<ActionResult<TaskItemModel>> AddTaskToBoard(
            [FromBody] AddTaskItemToBoardModel addTaskItemToBoardModel,
            [FromRoute] int boardId)
        {
            var taskId = await _boardService.AddTaskToBoard(boardId, addTaskItemToBoardModel);
            var task = await _taskItemProvider.GetTaskItem(taskId);
            
            return Ok(task);
        }

        //[HttpDelete]
        //[Route("{boardId}/tasks/{taskId}")]
        //public ActionResult RemoveTaskFromBoard(
        //    [FromRoute] int boardId,
        //    [FromRoute] int taskId)
        //{
        //    _boardRepository.RemoveTask(boardId, taskId);
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("{boardId}/members")]
        //public ActionResult<List<BoardMember>> GetBoardMembers(
        //    [FromRoute] int boardId)
        //{

        //    return Ok(_boardMemberRepository.GetBoardMembers(boardId));
        //}

        //[HttpPost]
        //[Route("{boardId}/members")]
        //public ActionResult AddBoardMember(
        //    [FromBody] int userId, //later will probably pass by email
        //    [FromRoute] int boardId)
        //{
        //    _boardMemberRepository.AddBoardMember(userId, boardId); //pass whole user as argument
        //    return Ok("Success");
        //}

        //[HttpDelete]
        //[Route("{boardId}/members")]
        //public ActionResult DeleteBoardMember(
        //    [FromBody] int userId, //later will probably pass by email
        //    [FromRoute] int boardId)
        //{
        //    _boardMemberRepository.DeleteBoardMember(userId, boardId); //pass whole user as argument
        //    return Ok("Success");
        //}
    }
}
