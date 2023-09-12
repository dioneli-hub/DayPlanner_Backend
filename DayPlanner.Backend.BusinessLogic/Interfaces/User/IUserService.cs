using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.User;
using DayPlanner.Backend.BusinessLogic.ServiceResponse;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserModel>> RegisterUser(CreateUserModel model);
        Task Verify(string verificationToken);
        Task<ServiceResponse<bool>> ForgotPassword(string email);
        Task<ServiceResponse<bool>> ResetPassword(ResetPasswordModel model);
        Task<ServiceResponse<UserModel>> TriggerVerification(VerifyUserModel model);
    }
}
