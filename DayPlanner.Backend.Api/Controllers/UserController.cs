using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.Domain;
using DayPlanner.Backend.ApiModels.User;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserService _userService;

        public UserController(
            IUserProvider userProvider,
            IUserService userService
            )
        {
            _userProvider = userProvider;
            _userService = userService;
        }

        [HttpPost(Name = nameof(RegisterUser))]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<UserModel>>> RegisterUser([FromBody] CreateUserModel model)
        {
            var userResponse = await _userService.RegisterUser(model);

            return Ok(userResponse);
        }

        [HttpPatch("verify", Name = nameof(Verify))]
        [AllowAnonymous]
        public async Task<ActionResult> Verify([FromBody] SmallTokenModel verificationToken)
        {

            //await _userService.Verify(WebUtility.UrlDecode(verificationToken.Token));
            await _userService.Verify(verificationToken.Token);

            return Ok("User successfully verified.");
        }


        [HttpPost("trigger-verification", Name = nameof(TriggerVerification))]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<UserModel>>> TriggerVerification([FromBody] VerifyUserModel model)
        {
            var response = await _userService.TriggerVerification(model);

            return Ok(response);
        }

        [HttpPatch("forgot-password", Name = nameof(ForgotPassword))]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<bool>>> ForgotPassword([FromBody] ForgotPasswordModel user)
        {
            var response = await _userService.ForgotPassword(user.Email);
            return Ok(response);
        }

        [HttpPatch("reset-password", Name = nameof(ResetPassword))]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<bool>>> ResetPassword([FromBody] ResetPasswordModel model)
        {

            var response = await _userService.ResetPassword(model);

            return Ok(response);
        }
    }
}

