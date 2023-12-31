﻿using DayPlanner.Backend.Api.Managers;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DayPlanner.Backend.BusinessLogic.Services.Auth
{
    public class JwtService : IJwtService
    {
        public TokenModel GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var nowUtc = DateTimeOffset.UtcNow;
            var expiresAt = nowUtc.AddDays(30);
            var tokenDescriptor = BuildSecurityTokenDescriptor(userId, nowUtc, expiresAt);
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new TokenModel
            {
                ExpiredAt = expiresAt,
                Token = token
            };
        }

        public bool IsValidAuthToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var key = Encoding.ASCII.GetBytes("GDxN28S3JvTRNqzGULCZvH9kzQ8qrxdB");
                var parameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = "DayPlanner.Issuer",
                    ValidateAudience = true,
                    ValidAudience = "DayPlanner.Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                tokenHandler.ValidateToken(token, parameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private SecurityTokenDescriptor BuildSecurityTokenDescriptor(int userId, DateTimeOffset nowUtc, DateTimeOffset expires)
        {
            var key = Encoding.ASCII.GetBytes("GDxN28S3JvTRNqzGULCZvH9kzQ8qrxdB");
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "DayPlanner.Issuer",
                Audience = "DayPlanner.Audience",
                NotBefore = nowUtc.UtcDateTime,
                Subject = new ClaimsIdentity(claims),
                Expires = expires.UtcDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }
    }
}
