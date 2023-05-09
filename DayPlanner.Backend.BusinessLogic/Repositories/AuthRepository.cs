﻿using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.Api.Managers;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.BusinessLogic.Managers;

namespace DayPlanner.Backend.BusinessLogic.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public TokenModel Authenticate(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null || ! PasswordManager.Verify(user, password))
            {
                throw new ApplicationException("Incorrect email or password.");
            }

            return JwtManager.GenerateJwtToken(user.Id);
        }
    }
}