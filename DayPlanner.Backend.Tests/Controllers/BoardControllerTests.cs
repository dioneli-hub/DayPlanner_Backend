
using DayPlanner.Backend.Api.Controllers;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DayPlanner.Backend.Tests.Controllers
{
    public class BoardControllerTests
    {
        private readonly IBoardProvider _boardProvider;
        private readonly IBoardService _boardService;
        private readonly ITaskItemProvider _taskItemProvider;
        public BoardControllerTests()
        {
            _boardProvider = A.Fake<IBoardProvider>();
            _boardService = A.Fake<IBoardService>();
            _taskItemProvider = A.Fake<ITaskItemProvider>();
        }

        [Fact]
        public async void BoardController_GetBoard_ReturnOkAndBoardModel()
        {
            //Arrange
            int boardId = 1;
            var controller = new BoardController(_boardProvider, _boardService, _taskItemProvider);
            var boardModel = A.Fake<BoardModel>();
            A.CallTo(() => _boardProvider.GetBoard(boardId)).Returns(boardModel);

            //Act
            var result = await controller.GetBoard(boardId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<BoardModel>));
            
        }

        [Fact]
        public async void BoardController_CreateBoard_ReturnOkAndBoardModel()
        {
            //Arrange
            var createBoardModel = A.Fake<CreateBoardModel>();
            var board = A.Fake<Board>();
            var boardModel = A.Fake<BoardModel>();

            A.CallTo(() =>_boardService.CreateBoard(createBoardModel)).Returns(board.Id);
            A.CallTo(() => _boardProvider.GetBoard(board.Id)).Returns(boardModel);

            var controller = new BoardController(_boardProvider, _boardService, _taskItemProvider);

            //Act
            var result = await controller.CreateBoard(createBoardModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<BoardModel>));

        }


    }
}
