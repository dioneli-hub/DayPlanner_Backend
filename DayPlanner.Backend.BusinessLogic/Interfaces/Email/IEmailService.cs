

using DayPlanner.Backend.ApiModels;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IEmailService
    {
       Task SendVerificationEmail(int userId);
       Task SendResetPasswordEmail(int userId);
    }
}
