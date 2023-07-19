using DayPlanner.Backend.Api.Controllers;
using DayPlanner.Backend.Api.Managers;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.Auth;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.Domain;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DayPlanner.Backend.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly IAuthService _authService;
        private readonly IUserContextService _userContextService;
        private readonly IUserProvider _userProvider;

        public AuthControllerTests()
        {
            _authService = A.Fake<IAuthService>();
            _userContextService = A.Fake<IUserContextService>(); 
            _userProvider = A.Fake<IUserProvider>();
        }

        [Fact]
        public async void AuthController_AuthenticatedUser_ReturnOkAndUserModel()
        {
            //Arrange
            int userId = 1;
            var userModel = A.Fake<UserModel>();

            A.CallTo(() => _userContextService.GetCurrentUserId()).Returns(userId);
            A.CallTo(() => _userProvider.GetUser(userId)).Returns(userModel);

            var controller = new AuthController(_authService, _userContextService, _userProvider);

            //Act
            var result = await controller.AuthenticatedUser();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<UserModel>));
        }
     

        [Fact]
        public async void AuthController_Authenticated_ReturnOkAndTokenResponse()
        {
            //Arrange
            var authModel = A.Fake<AuthenticateModel>();
            var tokenResponse = A.Fake<Domain.ServiceResponse<TokenModel>>();

            A.CallTo(() => _authService.Authenticate(authModel.Email, authModel.Password)).Returns(tokenResponse);

            var controller = new AuthController(_authService, _userContextService, _userProvider);

            //Act
            var result = await controller.Authenticate(authModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<Domain.ServiceResponse<TokenModel>>));
        }

    }
}
