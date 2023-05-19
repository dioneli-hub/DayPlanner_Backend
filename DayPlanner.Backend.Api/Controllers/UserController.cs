using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.ApiModels.User;
using DayPlanner.Backend.BusinessLogic.Interfaces;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IBoardMemberRepository _boardMemberRepository;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;
        private readonly IUserService _userService;

        public UserController(IUserRepository userRepository,
            IBoardMemberRepository boardMemberRepository, IMapper mapper, IUserProvider userProvider, IUserService userService)
        {
            _userRepository = userRepository;
            _boardMemberRepository = boardMemberRepository;
            _mapper = mapper;
            _userProvider = userProvider;
            _userService = userService;
        }

        [HttpGet(Name = nameof(GetAllUsers))] 
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            var users = await _userProvider.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{userId}", Name = nameof(GetUser))] // To change more
        public async Task<ActionResult<UserModel>> GetUser(int userId)
        {
            var user = await _userProvider.GetUser(userId);

            return Ok(user);
        }

        [HttpPost(Name = nameof(RegisterUser))]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> RegisterUser([FromBody] CreateUserModel model)
        {
            var userId = await _userService.RegisterUser(model);
            var user = await _userProvider.GetUser(userId);

            return Ok(user);
        }

        [HttpGet("{userId}/user-boards", Name = nameof(GetUserBoards))]
        public async Task<ActionResult<BoardModel>> GetUserBoards(int userId)
        {
            var userBoards = await _userProvider.GetUserBoards(userId);
            return Ok(userBoards);
        }
    }
}

