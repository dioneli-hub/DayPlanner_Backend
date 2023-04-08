using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.Api.DTOs
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BoardId { get; set; }

        public Board? Board { get; set; }
    }
}
