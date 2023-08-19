using DayPlanner.Backend.ApiModels.Recurrence;
using DayPlanner.Backend.ApiModels.TaskItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IRecurrenceService
    {
        Task<int> AddRecurrence(RecurringPatternModel patternModel);
        Task GenerateChildTasks(int patternId);
    }
}
