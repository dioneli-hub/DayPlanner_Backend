using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserModel>> RegisterUser(CreateUserModel model);
    }
}
