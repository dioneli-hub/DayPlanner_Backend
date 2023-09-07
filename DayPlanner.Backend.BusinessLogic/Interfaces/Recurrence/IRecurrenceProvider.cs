using DayPlanner.Backend.ApiModels.TaskItem;

namespace DayPlanner.Backend.BusinessLogic.Interfaces.Recurrence
{
    public interface IRecurrenceProvider
    {
       
        Task<List<TaskItemModel>> GetChildTasks(int parentTaskId);
    }
}
