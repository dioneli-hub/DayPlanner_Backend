
namespace DayPlanner.Backend.Domain
{
    public class TaskPerformer
    {
        public int TaskId { get; set; }
        public int PerformerId { get; set; }
        public TaskItem Task { get; set; }
        public User Performer { get; set; }
    }
}
