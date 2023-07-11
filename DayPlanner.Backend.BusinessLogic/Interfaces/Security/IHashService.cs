using DayPlanner.Backend.BusinessLogic.Models;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IHashService
    {
        HashModel Generate(string password);
        byte[] HashPassword(string password, byte[] salt);
    }
}
