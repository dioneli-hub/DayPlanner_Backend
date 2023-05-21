using DayPlanner.Backend.Api.Managers;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<TokenModel> Authenticate(string email, string password);
    }
}
