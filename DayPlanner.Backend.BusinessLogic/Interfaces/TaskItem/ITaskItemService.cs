using DayPlanner.Backend.ApiModels.TaskItem;


namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface ITaskItemService
    {
        Task DeleteTask(int taskId);
        Task UpdateTask(int taskId, EditTaskItemModel editTaskItemModel);
        Task CompleteTask(int taskId);
        Task MarkTaskAsToDo(int taskId);
    }
}
