

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IPasswordVerifier
    {
        Task<bool> Verify(int userId, string password);
    }
}
