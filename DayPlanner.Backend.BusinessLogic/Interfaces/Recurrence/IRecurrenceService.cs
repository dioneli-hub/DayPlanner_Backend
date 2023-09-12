using DayPlanner.Backend.ApiModels.Recurrence;
using DayPlanner.Backend.ApiModels.TaskItem;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IRecurrenceService
    {
        Task<int> AddRecurrence(RecurringPatternModel patternModel);
        Task<List<TaskItemModel>> GenerateChildTasks(int patternId);
        Task RescheduleChildTasks(int parentTaskId, TimeSpan timeShift);

    }
}
