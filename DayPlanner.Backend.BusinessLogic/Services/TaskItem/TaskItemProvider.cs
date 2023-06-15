using AutoMapper;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;

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
                .Include(x => x.Performer)
                .Include(x => x.Creator)
                .Where(t => t.Id == taskId)
                .FirstOrDefaultAsync();

            var taskModel = _mapper.Map<TaskItemModel>(task);

            return taskModel;
        }

        public async Task<List<TaskItemModel>> GetTasks()
        {
            var tasks = await _context.TaskItems
                .Include(x => x.Board)
                .Include(x => x.Performer)
                .Include(x => x.Creator)
                .ToListAsync();

            var taskModels = _mapper.Map<List<TaskItemModel>>(tasks);

            return taskModels;
        }

        public async Task<List<TaskItemModel>> GetTodaysTasks()
        {
            var todaysTasks = await  _context.TaskItems
                .Include(x => x.Board)
                .Include(x => x.Performer)
                .Include(x => x.Creator)
                .Where(item => item.DueDate >= DateTimeOffset.UtcNow.Date &&
                         item.DueDate < DateTimeOffset.UtcNow.Date.AddDays(1))
                .OrderBy(t => t.Id)
                .ToListAsync();

            var todaysTaskModels = _mapper.Map<List<TaskItemModel>>(todaysTasks);

            return todaysTaskModels;
        }

        public async Task<List<TaskItemModel>> GetUsersCompletedTasks(int userId)
        {
            var completedTasks = await _context.TaskItems
                .Include(x => x.Board)
                .Include(x => x.Performer)
                .Include(x => x.Creator)
                .Where(t => t.IsCompleted == true && (t.CreatorId == userId || t.PerformerId == userId))
                .OrderBy(t => t.Id)
                .ToListAsync();

            var completedTaskModels = _mapper.Map<List<TaskItemModel>>(completedTasks);

            return completedTaskModels;
        }

        public async Task<List<TaskItemModel>> GetUsersTasks(int userId)
        {
            var tasks = await _context.TaskItems
                .Include(x => x.Board)
                .Include(x => x.Performer)
                .Include(x => x.Creator)
                .Where(t => t.CreatorId == userId || t.PerformerId == userId)
                .OrderBy(t => t.Id)
                .ToListAsync();

            var taskModels = _mapper.Map<List<TaskItemModel>>(tasks);

            return taskModels;
        }

        public async Task<List<TaskItemModel>> GetUsersTodaysTasks(int userId)
        {
            var todaysTasks = await _context.TaskItems
                .Include(x => x.Board)
                .Include(x => x.Performer)
                .Include(x => x.Creator)
                .Where(t => (t.DueDate >= DateTimeOffset.UtcNow.Date &&
                         t.DueDate < DateTimeOffset.UtcNow.Date.AddDays(1))
                         && 
                         (t.CreatorId == userId || t.PerformerId == userId))
                .OrderBy(t => t.Id)
                .ToListAsync();

            var todaysTaskModels = _mapper.Map<List<TaskItemModel>>(todaysTasks);

            return todaysTaskModels;
        }

        public async Task<List<TaskItemModel>> GetUsersToDoTasks(int userId)
        {
            var toDoTasks = await _context.TaskItems
                .Include(x => x.Board)
                .Include(x => x.Performer)
                .Include(x => x.Creator)
                .Where(t => t.IsCompleted == false && (t.CreatorId == userId || t.PerformerId == userId))
                .OrderBy(t => t.Id)
                .ToListAsync();

            var toDoTaskModels = _mapper.Map<List<TaskItemModel>>(toDoTasks);

            return toDoTaskModels;
        }

        public async Task<List<TaskItemModel>> GetBoardTasks(int boardId)
        {
            var tasks = await _context.TaskItems
                .Include(x => x.Board)
                .Include(x => x.Performer)
                .Include(x => x.Creator)
                .Where(t => t.BoardId == boardId)
                .OrderByDescending(t => t.Id)
                .ToListAsync();

            var taskModels = _mapper.Map<List<TaskItemModel>>(tasks);

            return taskModels;

        }
    }
}
