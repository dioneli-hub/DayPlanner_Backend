using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.Api.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.Auth;
using DayPlanner.Backend.BusinessLogic.ServiceResponse;

namespace DayPlanner.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;
        private readonly IUserContextService _userContextService;
        private readonly IUserProvider _userProvider;

        public AuthController(IAuthService authService,
            IUserContextService userContextService,
            IUserProvider userProvider)
        {
            _authService = authService;
            _userContextService = userContextService;
            _userProvider = userProvider;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserModel>> AuthenticatedUser()
        {
            var userId = _userContextService.GetCurrentUserId();
            var user = await _userProvider.GetUser(userId);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<TokenModel>>> Authenticate(AuthenticateModel model)
        {
            var tokenResponse = await _authService.Authenticate(model.Email, model.Password);
            return Ok(tokenResponse);
        }
    }
}