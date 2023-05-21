

namespace DayPlanner.Backend.BusinessLogic.Interfaces.Security
{
    public interface IPasswordVerifier
    {
        Task<bool> Verify(int userId, string password);
    }
}
