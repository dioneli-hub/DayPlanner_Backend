using DayPlanner.Backend.ApiModels.Recurrence;
using DayPlanner.Backend.ApiModels.TaskItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.BusinessLogic.Interfaces.Recurrence
{
    public interface IRecurrenceService
    {
        Task<int> AddRecurrence(RecurringPatternModel patternModel);
        Task<List<TaskItemModel>> GenerateChildTasks(int patternId);
        Task RescheduleChildTasks(int parentTaskId, TimeSpan timeShift);

    }
}
