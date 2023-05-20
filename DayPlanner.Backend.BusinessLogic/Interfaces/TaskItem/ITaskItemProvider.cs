using DayPlanner.Backend.ApiModels.TaskItem;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface ITaskItemProvider
    {
        Task<TaskItemModel> GetTask(int taskId);
        Task<List<TaskItemModel>> GetTasks();
        Task<List<TaskItemModel>> GetTodaysTasks();
    }
}
