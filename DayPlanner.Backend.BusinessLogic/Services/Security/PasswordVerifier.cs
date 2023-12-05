using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services.Security
{
    public class PasswordVerifier : IPasswordVerifier
    {
        private readonly DataContext _context;
        private readonly IHashService _hashService;

        public PasswordVerifier(DataContext context,
            IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<bool> Verify(int userId, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return Verify(user, password);
        }

        private bool Verify(User user, string password)
        {
            var salt = Convert.FromBase64String(user.SaltHash);
            var hash = _hashService.HashPassword(password, salt);
            var hashedPasswordAsBase64String = Convert.ToBase64String(hash);

            return user.PasswordHash == hashedPasswordAsBase64String;
        }
    }
}
