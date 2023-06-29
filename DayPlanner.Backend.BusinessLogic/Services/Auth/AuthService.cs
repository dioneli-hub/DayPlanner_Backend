﻿using Azure;
using DayPlanner.Backend.Api.Managers;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;


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

        public async Task<ServiceResponse<TokenModel>> Authenticate(string email, string password)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                return new ServiceResponse<TokenModel>()
                {
                    IsSuccess = false,
                    Message = "User is not found. Please, check your email for correctness.",
                    Data = null
                };
            }
            else if (!await _passwordVerifier.Verify(user.Id, password))
            {
                return new ServiceResponse<TokenModel>()
                {
                    IsSuccess = false,
                    Message = "Wrong password. Please, enter again.",
                    Data = null,
                };
            }

            return new ServiceResponse<TokenModel>()
            {
                IsSuccess = true,
                Message = "User successfully authenticated!",
                Data = _jwtService.GenerateJwtToken(user.Id),
            };
        }
    }
}

            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            //if (user == null || !await _passwordVerifier.Verify(user.Id, password))
            //{
            //    throw new ApplicationException("Incorrect email or password.");
            //}

            //return _jwtService.GenerateJwtToken(user.Id);
