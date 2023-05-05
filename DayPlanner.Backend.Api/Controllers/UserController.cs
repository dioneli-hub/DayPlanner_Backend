using AutoMapper;
using DayPlanner.Backend.Api.Interfaces;
using DayPlanner.Backend.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
    }
}
