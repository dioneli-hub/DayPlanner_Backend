using DayPlanner.Backend.Api.Managers;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Security;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IJwtService _jwtService;
        private readonly IPasswordVerifier _passwordVerifier;

        public AuthService(DataContext context,
            IJwtService jwtService,
            IPasswordVerifier passwordVerifier)
        {
            _context = context;
            _jwtService = jwtService;
            _passwordVerifier = passwordVerifier;
        }

        public async Task<TokenModel> Authenticate(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || !await _passwordVerifier.Verify(user.Id, password))
            {
                throw new ApplicationException("Incorrect email or password.");
            }

            return _jwtService.GenerateJwtToken(user.Id);
        }
    }
}
