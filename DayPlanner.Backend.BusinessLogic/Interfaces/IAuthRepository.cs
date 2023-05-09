using DayPlanner.Backend.Api.Managers;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IAuthRepository
    {
        public TokenModel Authenticate(string email, string password);
    }
}
