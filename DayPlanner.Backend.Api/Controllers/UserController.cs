using AutoMapper;
using DayPlanner.Backend.Api.ApiModels;
using DayPlanner.Backend.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.Domain;


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

        public UserController(IUserRepository userRepository,
            IBoardMemberRepository boardMemberRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _boardMemberRepository = boardMemberRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetAllUsers))]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers(); // add mapper to simple user model

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(users);
            }
        }

        [HttpGet("{userId}", Name = nameof(GetUser))]
        public ActionResult<UserModel> GetUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var user = _mapper.Map<UserModel>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpPost(Name = nameof(RegisterUser))]
        [AllowAnonymous]
        public IActionResult RegisterUser([FromBody] CreateUserModel model)
        {
            if (_userRepository.UserIsRegistered(model.Email))
                return NotFound();


            if (model == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                if (!_userRepository.RegisterUser(model))
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully created");
        }

        [HttpGet("{userId}/boards", Name = nameof(GetUserMemberBoards))]
        public ActionResult<BoardModel> GetUserMemberBoards(int userId) 
        {
            return Ok(_boardMemberRepository.GetUserMemberBoards(userId));//mapping
        }

        [HttpGet("{userId}/boards", Name = nameof(GetCurrentUserMemberBoards))]
        public ActionResult<BoardModel> GetCurrentUserMemberBoards()
        {
            return Ok(_boardMemberRepository.GetCurrentUserMemberBoards());//mapping
        }
    }
}

