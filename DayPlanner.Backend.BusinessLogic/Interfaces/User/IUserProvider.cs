using DayPlanner.Backend.ApiModels;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IUserProvider
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUser(int userId);
        Task<List<BoardModel>> GetUserBoards(int userId);
    }
}
