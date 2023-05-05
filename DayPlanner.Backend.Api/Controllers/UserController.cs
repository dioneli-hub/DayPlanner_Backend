using AutoMapper;
using DayPlanner.Backend.Api.ApiModels;
using DayPlanner.Backend.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
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
            if (!_userRepository.UserIsRegistered(model.Email))
                return NotFound();


            if (model == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                if (!_userRepository.RegisterUser( model))
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully created");
            }
        }


    }

