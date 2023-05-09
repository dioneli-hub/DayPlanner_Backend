
using DayPlanner.Backend.Domain;
using DayPlanner.Backend.ApiModels.User;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
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
