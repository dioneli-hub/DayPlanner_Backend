using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using DayPlanner.Backend.BusinessLogic.Managers;
using DayPlanner.Backend.Domain;
using DayPlanner.Backend.BusinessLogic.Interfaces;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        //private readonly IUserContextService _userContextService;
        public UserService(DataContext context) //IUserContextService userContextService
        {
            _context = context;
        }
        public async Task<int> RegisterUser(CreateUserModel model)
        {
            var hasAnyByEmail = await _context.Users.AnyAsync(x => x.Email == model.Email);
            if (hasAnyByEmail)
            {
                throw new ApplicationException("User with the email provided already exists.");
            }

            var hashModel = HashManager.Generate(model.Password); // change for Hash service later

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = Convert.ToBase64String(hashModel.Hash),
                SaltHash = Convert.ToBase64String(hashModel.Salt),
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }
    }
}
