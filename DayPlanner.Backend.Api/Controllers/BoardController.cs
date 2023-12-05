using DayPlanner.Backend.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ITaskItemProvider _taskItemProvider;

        public BoardController(
            IBoardProvider boardProvider, 
            IBoardService boardService,
            ITaskItemProvider taskItemProvider)
        {
            _boardProvider = boardProvider;
            _boardService = boardService;
            _taskItemProvider = taskItemProvider;
            
        }

        [HttpGet("{boardId}", Name = nameof(GetBoard))]
        public async Task<ActionResult<BoardModel>> GetBoard(int boardId)
        {
            var board = await _boardProvider.GetBoard(boardId);
            
            return Ok(board);
        }

        //test
        [HttpGet("{boardId}/user/{userId}/is-allowed-to-board", Name = nameof(IsUserAllowedToBoard))]
        public async Task<ActionResult<bool>> IsUserAllowedToBoard(int boardId, int userId)
        {
            var isAllowed = await _boardProvider.IsUserAllowedToBoard(userId, boardId);

            return Ok(isAllowed);
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


        
        [HttpPost]
        [Route("{boardId}/tasks")]
        public async Task<ActionResult<TaskItemModel>> AddTaskToBoard(
            [FromBody] AddTaskItemToBoardModel addTaskItemToBoardModel,
            [FromRoute] int boardId)
        {
            var taskId = await _boardService.AddTaskToBoard(boardId, addTaskItemToBoardModel);
            var task = await _taskItemProvider.GetTask(taskId);

            return Ok(task);
        }



    }
}
