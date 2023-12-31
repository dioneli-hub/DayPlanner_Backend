﻿using DayPlanner.Backend.BusinessLogic.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DayPlanner.Backend.BusinessLogic.Services.Validation
{
    public class ValidationService : IValidationService
    {
        public bool ValidateEmail(string email)
        {
            var emailValidation = new EmailAddressAttribute();
            return emailValidation.IsValid(email);
        }

        public bool ValidatePassword(string password)
        {
            int validConditions = 0;
            foreach (char c in password)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in password)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions < 2) return false;
            foreach (char c in password)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions < 3) return false;
            if (validConditions == 3)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=', '!', '?' };
                if (password.IndexOfAny(special) == -1) return false;

                if (password.Length < 8) return false;
            }

            return true;
        }

    }
}
