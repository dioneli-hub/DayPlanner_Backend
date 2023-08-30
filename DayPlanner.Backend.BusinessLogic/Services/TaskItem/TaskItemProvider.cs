using AutoMapper;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Org.BouncyCastle.Tls;

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

        public async Task<List<TaskItemModel>> GetUserBoardsTasks(int userId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();


            var tasks = await _context.BoardMembers
                 .Where(bm => bm.MemberId == currentUserId)
                 .Select(bm => bm.Board)
                 .SelectMany(b => b.Tasks)
                 .Where(t => (t.PerformerId == currentUserId))
                 .Include(t => t.Performer)
                 .Include(t => t.Creator)
                 .Include(t => t.Board)
                 .OrderBy(t => t.Id)
                 .ToListAsync();

            var taskModels = _mapper.Map<List<TaskItemModel>>(tasks);
            

            return taskModels != null ? taskModels : new List<TaskItemModel>();
        }

        public async Task<List<TaskItemModel>> GetUserBoardsTodaysTasks(int userId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();

            var todaysTasks = await _context.BoardMembers
                 .Where(x => x.MemberId == currentUserId)
                 .Select(x => x.Board)
                 .SelectMany(x => x.Tasks)
                 .Where(t => (t.DueDate >= DateTimeOffset.UtcNow.Date &&
                         t.DueDate < DateTimeOffset.UtcNow.Date.AddDays(1))
                         && (t.PerformerId == currentUserId))
                 .Include(x => x.Performer)
                 .Include(x => x.Creator)
                 .Include(x => x.Board)
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

        public async Task<List<TaskItemModel>> GetBoardTasks(int boardId, bool ifMyTasks)
        {

            var query = _context.TaskItems
                .Include(x => x.Board)
                .Include(x => x.Performer)
                .Include(x => x.Creator)
                .Where(t => t.BoardId == boardId);

            if (ifMyTasks == true)
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                query = query.Where(x => x.PerformerId == currentUserId);
            }

            var tasks = await query
                .OrderByDescending(t => t.Id)
                .ToListAsync();

            var taskModels = _mapper.Map<List<TaskItemModel>>(tasks);

            return taskModels;
        }

        //public async Task<List<TaskGroup<TKey>>> GetGroupedAndFilteredBoardTasks<TKey>(int boardId, bool myTasks, string groupBy)
        //{

        //    var query =  _context.TaskItems
        //       .Include(x => x.Board)
        //       .Include(x => x.Performer)
        //       .Include(x => x.Creator)
        //       .AsQueryable();

        //    if(myTasks == true)
        //    {
        //        var currentUserId = _userContextService.GetCurrentUserId();
        //        query = query.Where(t => t.PerformerId == currentUserId);
        //    }

        //    var groupType = GetGroupType(groupBy);


        //    var taskGroups =await  query
        //        .GroupBy(GetGroupBySelector<TKey>(groupBy))
        //       .Select(group => new TaskGroup<TKey>
        //       {
        //           GroupKey = _mapper.Map<TKey>(group.Key),
        //           Tasks = _mapper.Map<List<TaskItemModel>>(group.ToList())
        //       }).ToListAsync();


        //    return taskGroups;
        //}


        //private Type GetGroupType(string groupBy)
        //{
        //    switch (groupBy.ToLower())
        //    {
        //        case "iscompleted":
        //            return typeof(bool);
        //        case "gender":
        //            return typeof(UserModel);

        //        default:
        //            return null;
        //    }
        //}
        //private Expression<Func<TaskItem, TKey>> GetGroupBySelector<TKey>(string groupBy)
        //{
        //    switch (groupBy.ToLower())
        //    {
        //        case "performer":
        //            return t => (TKey)Convert.ChangeType(t.Performer, typeof(TKey));
        //        case "iscompleted":
        //            return t => (TKey)Convert.ChangeType(t.IsCompleted, typeof(TKey));
        //        default:
        //            return s => (TKey)Convert.ChangeType(s.Id, typeof(TKey)); 
        //    }
        //}







        public async Task<List<TaskGroup<UserModel>>> GetBoardTasksGroupedByPerformer(int boardId)
        {
            var taskGroups = await _context.TaskItems
               .Include(x => x.Board)
               .Include(x => x.Performer)
               .Include(x => x.Creator)
               .Where(t => t.BoardId == boardId)
               .GroupBy(task => task.Performer)
               .Select(group => new TaskGroup<UserModel>
               {
                   GroupKey = _mapper.Map<UserModel>(group.Key),
                   //GroupName = group.FirstOrDefault()?.FirstName
                   Tasks = _mapper.Map<List<TaskItemModel>>(group.ToList())
               }).ToListAsync();


            return taskGroups;
        }


        public async Task<List<TaskGroup<bool>>> GetBoardTasksGroupedByCompleted(int boardId, bool ifMyTasks)
        {
            var query = _context.TaskItems
               .Include(x => x.Board)
               .Include(x => x.Performer)
               .Include(x => x.Creator)
               .Where(t => t.BoardId == boardId);

            if(ifMyTasks == true)
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                query = query.Where(x => x.PerformerId == currentUserId);
            }

               var taskGroups = await query
                .GroupBy(task => task.IsCompleted)
                .Select(group => new TaskGroup<bool>
               {
                   GroupKey = group.Key,
                   //GroupName = group.Key == true? "Done" : "To Do",
                   Tasks = _mapper.Map<List<TaskItemModel>>(group.ToList())
               }).ToListAsync();


            return taskGroups;
        }
    }

public class TaskGroup<T>
    {
        public T GroupKey { get; set; }
        //public string GroupName { get; set; }
        public List<TaskItemModel> Tasks { get; set; }
    }
}
