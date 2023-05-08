using DayPlanner.Backend.Api.ApiModels;
using DayPlanner.Backend.Api.Interfaces;
using DayPlanner.Backend.Api.Interfaces.Context;
using DayPlanner.Backend.Api.Managers;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public UserRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.Users
                //.Include(x => x.Avatar)
                .ToList();
        }

        public User GetUser(int userId)
        {
            var user = _context.Users
               //.Include(x => x.Avatar)
               .FirstOrDefault(x => x.Id == userId);

            return user!;
        }

        public bool RegisterUser(CreateUserModel model)
        {

            var hashModel = HashManager.Generate(model.Password);
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = Convert.ToBase64String(hashModel.Hash),
                SaltHash = Convert.ToBase64String(hashModel.Salt),
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.Users.Add(user);
            return Save();
        }

        public User GetCurrentUser() 
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var currentUser = _context.Users
               //.Include(x => x.Avatar)
               .FirstOrDefault(x => x.Id == currentUserId);
            return currentUser!;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(t => t.Id == userId);
        }

        public bool UserIsRegistered(string email)
        {
            var hasAnyByEmail = _context.Users.Any(x => x.Email == email);
            return hasAnyByEmail;
        }

}
}
