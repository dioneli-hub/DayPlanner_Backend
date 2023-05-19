using AutoMapper;
using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.ApiModels.User;
using DayPlanner.Backend.Domain;


namespace DayPlanner.Backend.Api.Helper.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
            CreateMap<BoardModel, Board>();
            CreateMap<TaskItem, TaskItemModel>();
            CreateMap<AddTaskItemToBoardModel, TaskItem>();
            CreateMap<AddTaskItemToBoardModel, TaskItemModel>();
            CreateMap<CreateBoardModel, Board>();
            CreateMap<EditTaskItemModel, TaskItem>();

            
            CreateMap<CreateUserModel, User>();


            CreateMap<User, UserModel>();
            CreateMap<Board, BoardModel>();
        }
    }
}
