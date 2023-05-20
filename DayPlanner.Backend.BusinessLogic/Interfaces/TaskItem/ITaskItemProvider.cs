using DayPlanner.Backend.ApiModels.TaskItem;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface ITaskItemProvider
    {
        Task<TaskItemModel> GetTaskItem(int taskId);
    }
}
