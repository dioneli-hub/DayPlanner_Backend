using DayPlanner.Backend.Api.Managers;

namespace DayPlanner.Backend.Api.Interfaces
{
    public interface IAuthRepository
    {
        public TokenModel Authenticate(string email, string password);
    }
}
