﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.Domain;
using DayPlanner.Backend.ApiModels.User;
using System.Linq.Expressions;
using System.Net;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UserController(
            IUserProvider userProvider,
            IUserService userService,
            IEmailService emailService
            )
        {
            _userProvider = userProvider;
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet(Name = nameof(GetAllUsers))]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            var users = await _userProvider.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{userId}", Name = nameof(GetUser))]
        public async Task<ActionResult<UserModel>> GetUser(int userId)
        {
            var user = await _userProvider.GetUser(userId);
            return Ok(user);
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

            await _userService.Verify(WebUtility.UrlDecode(verificationToken.Token));

            return Ok("User successfully verified.");
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


        //[HttpGet("{userId}/user-boards", Name = nameof(GetUserBoards))]
        //public async Task<ActionResult<BoardModel>> GetUserBoards(int userId)
        //{
        //    var userBoards = await _userProvider.GetUserBoards(userId);
        //    return Ok(userBoards);
        //}
    }
}

