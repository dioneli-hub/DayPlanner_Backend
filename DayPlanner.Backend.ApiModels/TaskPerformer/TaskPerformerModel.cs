using DayPlanner.Backend.ApiModels.TaskItem;

namespace DayPlanner.Backend.ApiModels.TaskPerformer
{
    public class TaskPerformerModel
    {
        public int TaskId { get; set; }
        public int PerformerId { get; set; }
        public TaskItemModel Task { get; set; }
        public UserModel Performer { get; set; }
    }
}
