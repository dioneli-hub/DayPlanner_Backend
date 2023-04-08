namespace DayPlanner.Backend.DataAccess.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BoardId { get; set; }

        public Board? Board { get; set; }
    }
}
