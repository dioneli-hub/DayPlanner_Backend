using AutoMapper;
using DayPlanner.Backend.Api.ApiModels;
using DayPlanner.Backend.Api.DTOs;
using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Board, BoardDto>();
            CreateMap<BoardDto, Board>();
            CreateMap<TaskItem, TaskItemDto>();
            CreateMap<AddTaskItemToBoardModel, TaskItem>();
            CreateMap<EditTaskItemModel, TaskItem>();
        }
    }
}
