using DayPlanner.Backend.Api.Managers;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.Api.Interfaces
{
    public interface IAuthRepository
    {
        public TokenModel Authenticate(string email, string password);
    }
}
