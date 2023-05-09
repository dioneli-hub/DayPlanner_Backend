using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface ITaskItemRepository
    {
        ICollection<TaskItem> GetTasks();
        ICollection<TaskItem> GetTodaysTasks();
        TaskItem GetTaskItem(int taskItemId);
        bool TaskItemExists(int taskItemId);
        bool UpdateTask(TaskItem task);
        bool DeleteTaskItem(TaskItem task);
    }
}
