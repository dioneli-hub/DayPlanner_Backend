namespace DayPlanner.Backend.ApiModels.TaskItem
{
    public class TaskItemModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsRecurring { get; set; }
        public bool ChangeRecurredChildren { get; set; }
        public int CreatorId { get; set; }
        public UserModel Creator { get; set; }
        public int BoardId { get; set; }
        public BoardModel Board { get; set; }
        public int PerformerId { get; set; }
        public UserModel Performer { get; set; }
        public bool IsOverdue{ get; set; }
        public int ParentTaskId { get; set; }

    }
}
