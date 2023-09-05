using AutoMapper;
using DayPlanner.Backend.ApiModels.Recurrence;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.BusinessLogic.Interfaces.Recurrence;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DayPlanner.Backend.BusinessLogic.Services.Recurrence.RecurrenceService;

namespace DayPlanner.Backend.BusinessLogic.Services.Recurrence
{
    public class RecurrenceService : IRecurrenceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RecurrenceService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddRecurrence(RecurringPatternModel patternModel)
        {
            var task = await _context.TaskItems.SingleOrDefaultAsync(x => x.Id == patternModel.TaskId);
            if (task != null)
            {
                task.IsRecurring = true;

                RecurringType recurringType; 

                switch (patternModel.RecurringType.ToLower())
                {
                    case "daily":
                        recurringType = RecurringType.DAILY;
                        break;
                    case "weekly":
                        recurringType = RecurringType.WEEKLY;
                        break;
                    case "monthly":
                        recurringType = RecurringType.MONTHLY;
                        break;
                    default:
                        recurringType = RecurringType.WEEKLY;
                        break;
                }

                var pattern = new RecurringPattern
                {
                    TaskId = patternModel.TaskId,
                    RecurringType = recurringType,
                    OccurencesNumber = patternModel.OccurencesNumber
                };

                await _context.RecurringPatterns.AddAsync(pattern);
                _context.Update(task);
                await _context.SaveChangesAsync();

                //await GenerateChildTasks(patternModel.TaskId);
                return pattern.Id;
            }
            throw new ApplicationException("Something went wrong during setting recurrence.");
        }


        public async Task<List<TaskItemModel>> GenerateChildTasks(int patternId)
        {
            

            var pattern = await _context.RecurringPatterns.SingleOrDefaultAsync(x => x.Id == patternId);

            if (pattern == null)
            {
                throw new ApplicationException("Recurrence pattern with such ID not found.");
            }

            var task = await _context.TaskItems.SingleOrDefaultAsync(x => x.Id == pattern.TaskId);
            
            if (task == null)
            {
                throw new ApplicationException("Parent task with such ID not found");
            }


            DateTimeOffset currentDueDate = task.DueDate;
            var tasksList = new List<TaskItem>(); ;
            var taskModelsList = new List<TaskItemModel>();

            for (int i = 0; i < pattern.OccurencesNumber; i++)
            {

                switch (pattern.RecurringType)
                {
                    case RecurringType.DAILY:
                        currentDueDate = currentDueDate.AddDays(1);
                        break;
                    case RecurringType.WEEKLY:
                        currentDueDate = currentDueDate.AddDays(7);
                        break;
                    case RecurringType.MONTHLY:
                        currentDueDate = currentDueDate.AddMonths(1);
                        break;
                }

                

                var childTask = new TaskItem
                {
                    Text = task.Text,
                    DueDate = currentDueDate,
                    CreatedAt = DateTimeOffset.UtcNow,
                    CreatorId = task.CreatorId,
                    Creator = await _context.Users.Where(x => x.Id == task.CreatorId).FirstOrDefaultAsync(),//task.Creator,
                    BoardId = task.BoardId,
                    Board = await _context.Boards.Where(x => x.Id == task.BoardId).FirstOrDefaultAsync(),
                    PerformerId = task.PerformerId,
                    Performer = await _context.Users.Where(x => x.Id == task.PerformerId).FirstOrDefaultAsync(),
                    ParentTaskId = task.Id
                };

                

                await _context.TaskItems.AddAsync(childTask);
                await _context.SaveChangesAsync();
                tasksList.Add(childTask);
            }

            taskModelsList = _mapper.Map<List<TaskItemModel>>(tasksList);
            
            return taskModelsList;
        }

        public async Task RescheduleChildTasks(int parentTaskId, TimeSpan timeShift)
        {
            try
            {
                var childTasks = await _context.TaskItems
                                     .Where(x => x.ParentTaskId == parentTaskId)
                                     .ToListAsync();

                foreach (var childTask in childTasks)
                {
                    childTask.DueDate = childTask.DueDate + timeShift;
                    _context.Update(childTask);
                }
            } catch
            {
                throw new ApplicationException("Something went wrong during rescheduling child tasks...");
            };

        }
    }

}
