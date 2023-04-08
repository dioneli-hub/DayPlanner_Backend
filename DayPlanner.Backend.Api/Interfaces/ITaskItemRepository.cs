
using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.Api.Interfaces
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
