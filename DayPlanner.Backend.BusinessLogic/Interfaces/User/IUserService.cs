using DayPlanner.Backend.ApiModels;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(CreateUserModel model);
    }
}
