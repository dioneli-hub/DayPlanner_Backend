namespace DayPlanner.Backend.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public int? PerformerId { get; set; } = null;
        public User? Performer { get; set; } = null;
    }
}
