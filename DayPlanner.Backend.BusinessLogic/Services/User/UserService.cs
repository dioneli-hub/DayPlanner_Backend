using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.User;
using Microsoft.EntityFrameworkCore;
using DayPlanner.Backend.BusinessLogic.ServiceResponse;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHashService _hashService;
        private readonly IUserProvider _userProvider;
        private readonly IValidationService _validationService;
        private readonly IEmailService _emailService;
        public UserService(
            DataContext context,
            IHashService hashService,
            IUserProvider userProvider,
            IValidationService validationService,
            IEmailService emailService) 
        {
            _context = context;
            _hashService = hashService;
            _userProvider = userProvider;
            _validationService = validationService;
            _emailService = emailService;


        }

        public async Task<ServiceResponse<UserModel>> RegisterUser(CreateUserModel model)
        {

            var hasAnyByEmail = await _userProvider.UserExists(model.Email);

            if (hasAnyByEmail)
            {
                return new ServiceResponse<UserModel>()
                {
                    IsSuccess = false,
                    Message = "The user with this email already exists.",
                    Data = null
                };
            }

            var isPasswordValid = _validationService.ValidatePassword(model.Password);

            if (!isPasswordValid)
            {
                return new ServiceResponse<UserModel>()
                {
                    IsSuccess = false,
                    Message = "Your password is not valid. Make sure it is at lest 8 characters long and contains one uppercase letter, one lowercase letter, one number and one special character ('@', '#', '$', '%', '^', '&', '+', '=', '!', '?').",
                    Data = null
                };
            }

            var isUserEmailValid = _validationService.ValidateEmail(model.Email);

            if (!isUserEmailValid)
            {
                return new ServiceResponse<UserModel>()
                {
                    IsSuccess = false,
                    Message = "Your email is not valid. Please, check it for correctness.",
                    Data = null
                };
            }
            var hashModel = _hashService.Generate(model.Password);
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = Convert.ToBase64String(hashModel.Hash),
                SaltHash = Convert.ToBase64String(hashModel.Salt),
                CreatedAt = DateTimeOffset.UtcNow,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await GenerateAndSendVerificationToken(user);

            return new ServiceResponse<UserModel>()
            {
                IsSuccess = true,
                Message = "User successfully registered. Email verification is needed. Please, check your mailbox to confirm your email address.",
                Data = await _userProvider.GetUser(user.Id)
            };
        }

        public async Task<ServiceResponse<UserModel>> TriggerVerification(VerifyUserModel model)
        {
            try
            {
                var hasAnyByEmail = await _userProvider.UserExists(model.Email);

                if (!hasAnyByEmail)
                {
                    return new ServiceResponse<UserModel>()
                    {
                        IsSuccess = false,
                        Message = "The user with this email does not exist.",
                        Data = null
                    };
                }

                var user = await _context.Users
                                    .Where(x => x.Email == model.Email)
                                    .FirstOrDefaultAsync();

                await GenerateAndSendVerificationToken(user);

                return new ServiceResponse<UserModel>()
                {
                    IsSuccess = true,
                    Message = "New email verification message sent to the your mailbox. Please, check it to confirm your email address.",
                    Data = await _userProvider.GetUser(user.Id)
                };
            }
            catch
            {
                return new ServiceResponse<UserModel>()
                {
                    IsSuccess = false,
                    Message = "Something went wrong. Please, make sure the email entered is valid and the user is already registered.",
                    Data = null
                };
            }
        }


        private async Task GenerateAndSendVerificationToken(User user)
        {
            try
            {
                user.VerificationToken = _hashService.GenerateRandomToken(64);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                await _emailService.SendVerificationEmail(user.Id);
            }
            catch
            {
                throw new ApplicationException("Some error has occured while trying to send verification token to the user's mailbox.");
            }
        }

        public async Task Verify(string verificationToken)
        {
            try
            {
                var user = _context.Users
               .FirstOrDefault(x => x.VerificationToken == verificationToken);

                if (user == null)
                {
                    throw new ApplicationException("Could not verify unexisting user.");
                }
                user.VerifiedAt = DateTimeOffset.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new ApplicationException("Some error has occured while attempring to verify the user.");
            }
           
        }

        public async Task<ServiceResponse<bool>> ForgotPassword(string email)
        {

            var user = _context.Users
                .FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return new ServiceResponse<bool>()
                {
                    IsSuccess = false,
                    Message = "Could not find the user. Please, make sure the email you entered exists and try again.",
                    Data = true
                };
            }
            user.ResetPasswordToken = _hashService.GenerateRandomToken(64);
            user.ResetPasswrodTokenExpiresAt = DateTimeOffset.UtcNow.AddDays(1);
            await _context.SaveChangesAsync();
            await _emailService.SendResetPasswordEmail(user.Id);

            return new ServiceResponse<bool>()
            {
                IsSuccess = true,
                Message = "A reset password email successfylly sent to your mailbox.",
                Data = true
            };
        }

        public async Task<ServiceResponse<bool>> ResetPassword(ResetPasswordModel model)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.ResetPasswordToken == model.ResetPasswordToken);

            if (user == null || user.ResetPasswrodTokenExpiresAt < DateTime.UtcNow)
            {
                return new ServiceResponse<bool>()
                {
                    IsSuccess = false,
                    Message = "The token is invalid.",
                    Data = true
                };
            }

            var isPasswordValid = _validationService.ValidatePassword(model.NewPassword);

            if (!isPasswordValid)
            {
                return new ServiceResponse<bool>()
                {
                    IsSuccess = false,
                    Message = "Your password is not valid. Make sure it is at lest 8 characters long and contains one uppercase letter, one lowercase letter, one number and one special character ('@', '#', '$', '%', '^', '&', '+', '=', '!', '?').",
                    Data = true
                };
            }


            var hashModel = _hashService.Generate(model.NewPassword);

            user.PasswordHash = Convert.ToBase64String(hashModel.Hash);
            user.SaltHash = Convert.ToBase64String(hashModel.Salt);
            user.ResetPasswordToken = null;
            user.ResetPasswrodTokenExpiresAt = null;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>()
            {
                IsSuccess = true,
                Message = "Password successfully changed.",
                Data = true
            };
        }
    }
}
