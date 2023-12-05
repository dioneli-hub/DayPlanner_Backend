using DayPlanner.Backend.ApiModels.Auth;
using DayPlanner.Backend.ApiModels.User;
using Microsoft.AspNetCore.Mvc;

namespace DayPlanner.Backend.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserService _userService;

        public UserControllerTests()
        {
            _userProvider = A.Fake<IUserProvider>();
            _userService = A.Fake<IUserService>();

        }

        [Fact]
        public async void UserController_RegisterUser_ReturnOkAndUserResponse()
        {
            //Arrange
            var model = A.Fake<CreateUserModel>();

            A.CallTo(() => _userService.RegisterUser(model)).Returns(new ServiceResponse<UserModel>());
            var controller = new UserController(_userProvider, _userService);

            //Act
            var result = await controller.RegisterUser(model);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<ServiceResponse<UserModel>>));
        }

        [Fact]
        public async void TUserController_Verify_ReturnOk()
        {
            //Arrange
            var verificationToken = A.Fake<SmallTokenModel>();
            A.CallTo(() => _userService.Verify(verificationToken.Token)).Returns(Task.CompletedTask);
            var controller = new UserController(_userProvider, _userService);

            //Act
            var result = await controller.Verify(verificationToken);

            var okResult = result as OkObjectResult;
            //Assert
            okResult.Should().NotBeNull();
            okResult?.Value.Should().Be("User successfully verified.");
        }


        [Fact]
        public async void UserController_TriggerVerification_ReturnOkAndUserResponse()
        {
            //Arrange
            var model = A.Fake<VerifyUserModel>();

            A.CallTo(() => _userService.TriggerVerification(model)).Returns(new ServiceResponse<UserModel>());
            var controller = new UserController(_userProvider, _userService);

            //Act
            var result = await controller.TriggerVerification(model);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<ServiceResponse<UserModel>>));
        }


        [Fact]
        public async void UserController_ForgotPassword_ReturnOkAndUserResponse()
        {
            //Arrange
            var model = A.Fake<ForgotPasswordModel>();

            A.CallTo(() => _userService.ForgotPassword(model.Email)).Returns(new ServiceResponse<bool>());
            var controller = new UserController(_userProvider, _userService);

            //Act
            var result = await controller.ForgotPassword(model);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<ServiceResponse<bool>>));
        }

        [Fact]
        public async void UserController_ResetPassword_ReturnOkAndUserResponse()
        {
            //Arrange
            var model = A.Fake<ResetPasswordModel>();

            A.CallTo(() => _userService.ResetPassword(model)).Returns(new ServiceResponse<bool>());
            var controller = new UserController(_userProvider, _userService);

            //Act
            var result = await controller.ResetPassword(model);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<ServiceResponse<bool>>));
        }
    }
}
