using AutoMapper;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.Api.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.Auth;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;

namespace DayPlanner.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly IAuthRepository _authRepository;
        private readonly IUserContextService _userContextService;
        private readonly IUserProvider _userProvider;

        public AuthController(IAuthRepository authRepository,
            IUserContextService userContextService,
            IUserProvider userProvider)
        {
            _authRepository = authRepository;
            _userContextService = userContextService;
            _userProvider = userProvider;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserModel>> AuthenticatedUser()
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var user = await _userProvider.GetUser(currentUserId);

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<TokenModel> Authenticate(AuthenticateModel model) // make async later
        {
            var token = _authRepository.Authenticate(model.Email, model.Password);
            return Ok(token);
        }

        
    }
}