using DayPlanner.Backend.ApiModels.Recurrence;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
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
        public RecurrenceService(DataContext context)
        {
            _context = context;
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

        //public async RecurringPatternModel R(RecurringPatternModel patternModel)
        //{

        //}

        public async Task GenerateChildTasks(int patternId)
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

            //List<TaskItem> childTasks = new List<TaskItem>();

            DateTimeOffset currentDueDate = task.DueDate;

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
                    Creator = task.Creator,
                    BoardId = task.BoardId,
                    Board = task.Board,
                    PerformerId = task.PerformerId,
                    Performer = task.Performer,
                    ParentTaskId = task.Id
                };

                //childTasks.Add(childTask);
                await _context.TaskItems.AddAsync(childTask);

            }
            await _context.SaveChangesAsync();
            //return childTasks;
        }
    }

}
