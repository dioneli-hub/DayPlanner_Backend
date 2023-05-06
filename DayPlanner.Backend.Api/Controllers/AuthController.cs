using AutoMapper;
using DayPlanner.Backend.Api.ApiModels;
using DayPlanner.Backend.Api.Interfaces;
using DayPlanner.Backend.Api.Managers;
using DayPlanner.Backend.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DayPlanner.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthRepository authRepository,
            IUserRepository userContextService)
        {
            _authRepository = authRepository;
            _userRepository = userContextService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<UserModel> AuthenticatedUser()
        {
            var userId = this.CurrentUserId;
            var user = _userRepository.GetUser(userId);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<TokenModel> Authenticate(AuthenticateModel model)
        {
            var token = _authRepository.Authenticate(model.Email, model.Password);
            return Ok(token);
        }

        public int CurrentUserId
        {
            get
            {
                var nameClaim = HttpContext.User.Identity!.Name;
                return int.Parse(nameClaim!);

            }
        }
    }
}