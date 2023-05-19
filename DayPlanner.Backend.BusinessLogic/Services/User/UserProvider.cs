using AutoMapper;
using DayPlanner.Backend.ApiModels.User;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.BusinessLogic.Interfaces;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class UserProvider : IUserProvider
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        //private readonly IUserContextService _userContextService;
        public UserProvider(DataContext context,
            IMapper mapper) //IUserContextService userContextService
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserModel> GetUser(int userId)
        {
            var user = await _context.Users
            //.Include(x => x.Avatar)
               .FirstOrDefaultAsync(x => x.Id == userId);

            var userModel =_mapper.Map<UserModel>(user);

            return userModel!;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = await _context.Users
            //.Include(x => x.Avatar)
                .ToListAsync();

            var userModels = _mapper.Map<List<UserModel>>(users);

            return userModels;
        }

        public async Task<List<BoardModel>> GetUserBoards(int userId)
        {
            var boards = await _context.Boards
                .Where(x => x.CreatorId == userId)
                .ToListAsync();

            var boardModels = _mapper.Map<List<BoardModel>>(boards);

            return boardModels;
        }
    }
}
