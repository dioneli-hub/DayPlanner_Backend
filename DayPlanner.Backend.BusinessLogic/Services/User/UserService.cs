using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.ApiModels;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHashService _hashService;
        private readonly IUserProvider _userProvider;
        private readonly IValidationService _validationService;
        public UserService(
            DataContext context,
            IHashService hashService,
            IUserProvider userProvider,
            IValidationService validationService) 
        {
            _context = context;
            _hashService = hashService;
            _userProvider = userProvider;
            _validationService = validationService;
                
        }
        //public async Task<int> RegisterUser(CreateUserModel model)
        //{
        //    var hasAnyByEmail = await _context.Users.AnyAsync(x => x.Email == model.Email);
        //    if (hasAnyByEmail)
        //    {
        //        throw new ApplicationException("User with the email provided already exists.");
        //    }

        //    var hashModel = _hashService.Generate(model.Password); 

        //    var user = new User
        //    {
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        Email = model.Email,
        //        PasswordHash = Convert.ToBase64String(hashModel.Hash),
        //        SaltHash = Convert.ToBase64String(hashModel.Salt),
        //        CreatedAt = DateTimeOffset.UtcNow
        //    };

        //    await _context.Users.AddAsync(user);
        //    await _context.SaveChangesAsync();

        //    return user.Id;
        //}

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
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<UserModel>()
            {
                IsSuccess = true,
                Message = "User successfully registered.",
                Data = await _userProvider.GetUser(user.Id)
            };
        }
    }
}
