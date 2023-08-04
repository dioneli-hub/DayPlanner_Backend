using DayPlanner.Backend.Api.Controllers;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DayPlanner.Backend.Tests.Controllers
{
    public class BoardMemberControllerTests
    {
        private readonly IBoardMemberProvider _boardMemberProvider;
        private readonly IBoardMemberService _boardMemberService;
        private readonly IUserProvider _userProvider;

        public BoardMemberControllerTests()
        {
            _userProvider = A.Fake<IUserProvider>();
            _boardMemberProvider = A.Fake<IBoardMemberProvider>();
            _boardMemberService = A.Fake<IBoardMemberService>();
        }

        [Fact] 
        public async void BoardMemberController_GetBoardMembers_ReturnOkAndUserModelList()
        {
            //Arrange
            int boardId = 1;
            var boardModel = A.Fake<BoardModel>();
            A.CallTo(() => _boardMemberProvider.GetBoardMembers(boardId)).Returns(new List<UserModel>());
            var controller = new BoardMemberController(_boardMemberService, _boardMemberProvider, _userProvider);

            //Act
            var result = await controller.GetBoardMembers(boardId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<List<UserModel>>));
        }


        [Fact] //improve test
        public async void BoardMemberController_AddBoardMemberByEmail_ReturnOkAndUserModel()
        {
            //Arrange
            int boardId = 1;
            int userId = 1;
            string email = "email@example.com";
            var userModel = A.Fake<UserModel>();
            var boardModel = A.Fake<BoardModel>();

            //A.CallTo(() => _boardMemberService.AddBoardMemberByEmail(boardId, email)).Returns(userId);
            A.CallTo(() => _userProvider.GetUser(userId)).Returns(userModel);

            var controller = new BoardMemberController(_boardMemberService, _boardMemberProvider, _userProvider);

            //Act
            var result = await controller.AddBoardMemberByEmail(boardId, email);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<UserModel>));
        }

        [Fact]
        public async void BoardMemberController_DeleteBoardMember_ReturnOk()
        {
            //Arrange

            int boardId = 2;
            int userId = 1;
            A.CallTo(() => _boardMemberService.DeleteBoardMember(boardId, userId)).GetType().Should().NotBeNull();
            var controller = new BoardMemberController(_boardMemberService, _boardMemberProvider, _userProvider);

            //Act
            var result = await controller.DeleteBoardMember(boardId, userId);


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public async void BoardMemberController_LeaveBoard_ReturnOk()
        {
            //Arrange

            int boardId = 1;
            int userId = 2;
            A.CallTo(() => _boardMemberService.LeaveBoard(userId, boardId)).GetType().Should().NotBeNull();
            var controller = new BoardMemberController(_boardMemberService, _boardMemberProvider, _userProvider);

            //Act
            var result = await controller.LeaveBoard(userId, boardId);


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public async void BoardMemberController_GetUserBoards_ReturnOkAndBoardModelList()
        {
            //Arrange
            int userId = 1;
            A.CallTo(() => _boardMemberProvider.GetUserBoards(userId)).Returns(new List<BoardModel>());
            var controller = new BoardMemberController(_boardMemberService, _boardMemberProvider, _userProvider);

            //Act
            var result = await controller.GetUserBoards(userId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<List<BoardModel>>));
        }


    }
}
