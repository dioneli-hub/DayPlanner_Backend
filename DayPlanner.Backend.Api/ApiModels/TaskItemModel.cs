namespace DayPlanner.Backend.Api.ApiModels
{
    public class TaskItemModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BoardId { get; set; }
        public BoardModel? Board { get; set; }

        public int CreatorId { get; set; }

    }
}
