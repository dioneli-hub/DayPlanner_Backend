using AutoMapper;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class TaskItemProvider : ITaskItemProvider
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TaskItemProvider(DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TaskItemModel> GetTask(int taskId)
        {
            var task = await _context.TaskItems
                .Include(x => x.Board)
                .ThenInclude(x => x.Creator)
                .Where(t => t.Id == taskId)
                .FirstOrDefaultAsync();

            var taskModel = _mapper.Map<TaskItemModel>(task);

            return taskModel;
        }

        public async Task<List<TaskItemModel>> GetTasks()
        {
            var tasks = await _context.TaskItems
                .Include(x => x.Board)
                .ThenInclude(x => x.Creator)
                .ToListAsync();

            var taskModels = _mapper.Map<List<TaskItemModel>>(tasks);

            return taskModels;
        }

        public async Task<List<TaskItemModel>> GetTodaysTasks()
        {
            var todaysTasks = await  _context.TaskItems
                .Include(x => x.Board)
                .ThenInclude(x => x.Creator)
                .Where(item => item.DueDate >= DateTimeOffset.UtcNow.Date &&
                         item.DueDate < DateTimeOffset.UtcNow.Date.AddDays(1))
                .OrderBy(t => t.Id)
                .ToListAsync();

            var todaysTaskModels = _mapper.Map<List<TaskItemModel>>(todaysTasks);

            return todaysTaskModels;
        }
    }
}
