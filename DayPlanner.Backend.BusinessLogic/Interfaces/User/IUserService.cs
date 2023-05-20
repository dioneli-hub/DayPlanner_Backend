using DayPlanner.Backend.ApiModels.User;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(CreateUserModel model);
    }
}
