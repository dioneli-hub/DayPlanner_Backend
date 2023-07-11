using DayPlanner.Backend.Api.Managers;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        TokenModel GenerateJwtToken(int userId);
        bool IsValidAuthToken(string token);
    }
}
