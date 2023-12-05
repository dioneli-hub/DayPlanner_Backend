using DayPlanner.Backend.ApiModels.TaskItem;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface ITaskItemService
    {
        Task DeleteTask(int taskId);
        Task<int> UpdateTask(int taskId, EditTaskItemModel editTaskItemModel);
        Task UpdateTaskPerformer(int taskId, int newPerformerId);
        Task CompleteTask(int taskId);
        Task MarkTaskAsToDo(int taskId);
        Task AssignTaskPerformer(int taskId, int performerId);
        Task RemoveTaskPerformer(int taskId);
        Task<bool> UpdateChangeRecurredChildren(int taskId);
        Task UpdateTaskOverdue(int taskId);
    }
}
