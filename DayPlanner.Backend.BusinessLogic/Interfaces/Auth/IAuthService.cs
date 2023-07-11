using DayPlanner.Backend.Api.Managers;
using DayPlanner.Backend.Domain;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<TokenModel>> Authenticate(string email, string password);
    }
}
