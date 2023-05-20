using AutoMapper;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.Domain;
using DayPlanner.Backend.ApiModels.BoardMember;

namespace DayPlanner.Backend.Api.Helper.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
            CreateMap<BoardModel, Board>();
            
            CreateMap<AddTaskItemToBoardModel, TaskItem>();
            CreateMap<AddTaskItemToBoardModel, TaskItemModel>();
            CreateMap<CreateBoardModel, Board>();
            CreateMap<EditTaskItemModel, TaskItem>();

            
            CreateMap<CreateUserModel, User>();


            CreateMap<User, UserModel>();
            CreateMap<Board, BoardModel>();
            CreateMap<TaskItem, TaskItemModel>();
            CreateMap<BoardMember, BoardMemberModel>();
        }
    }
}
