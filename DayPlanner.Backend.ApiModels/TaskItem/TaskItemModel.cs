using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.ApiModels.TaskItem
{
    public class TaskItemModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public int BoardId { get; set; }
        public BoardModel Board { get; set; }
        public int CreatorId { get; set; }
        public UserModel Creator { get; set; }
        public int? PerformerId { get; set; }
        public User? Performer { get; set; }

    }
}
