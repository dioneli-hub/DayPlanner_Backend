using DayPlanner.Backend.Api.ApiModels;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.Api.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUsers();
        User GetUser(int userId);

        bool RegisterUser(CreateUserModel model);

        bool UserIsRegistered(string email);

        bool UserExists(int userId);

        User GetCurrentUser();
    }
}
