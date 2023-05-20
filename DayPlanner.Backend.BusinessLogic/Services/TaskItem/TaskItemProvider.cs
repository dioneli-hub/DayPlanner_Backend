using AutoMapper;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class TaskItemProvider : ITaskItemProvider
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public TaskItemProvider(DataContext context,
            IMapper mapper,
            IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public async Task<TaskItemModel> GetTaskItem(int taskId)
        {
            var task = await _context.TaskItems
                .Where(t => t.Id == taskId)
                .FirstOrDefaultAsync();

            var taskModel = _mapper.Map<TaskItemModel>(task);

            return taskModel;
        }
    }
}
