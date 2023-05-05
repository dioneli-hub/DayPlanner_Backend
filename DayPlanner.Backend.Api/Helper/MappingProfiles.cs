using AutoMapper;
using DayPlanner.Backend.Api.ApiModels;
using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Board, BoardModel>();
            CreateMap<BoardModel, Board>();
            CreateMap<TaskItem, TaskItemModel>();
            CreateMap<AddTaskItemToBoardModel, TaskItem>();
            CreateMap<AddTaskItemToBoardModel, TaskItemModel>();
            CreateMap<CreateBoardModel, Board>();
            CreateMap<EditTaskItemModel, TaskItem>();

            CreateMap<User, UserModel>();
            CreateMap<CreateUserModel, User>();
        }
    }
}
