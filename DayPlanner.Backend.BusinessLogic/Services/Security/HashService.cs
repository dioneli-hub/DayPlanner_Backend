using DayPlanner.Backend.BusinessLogic.Interfaces.Security;
using DayPlanner.Backend.BusinessLogic.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;


namespace DayPlanner.Backend.BusinessLogic.Services.Security
{
    public class HashService : IHashService
    {
        private const int SaltSize = 16;
        private const int IterationsCount = 100;
        private const int KeySizeInBytes = 32;

        public HashModel Generate(string password)
        {
            var salt = GenerateSalt(SaltSize);

            return new HashModel
            {
                Salt = salt,
                Hash = HashPassword(password, salt)
            };
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(password, salt,
                KeyDerivationPrf.HMACSHA512,
                IterationsCount,
                KeySizeInBytes);
        }

        private static byte[] GenerateSalt(int saltSize)
        {
            var salt = new byte[saltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }
    }
}
