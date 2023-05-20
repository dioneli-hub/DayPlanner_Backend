﻿using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.User;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IUserProvider
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUser(int userId);
        Task<List<BoardModel>> GetUserBoards(int userId);
    }
}
