using DayPlanner.Backend.ApiModels.Board;

namespace DayPlanner.Backend.ApiModels.TaskItem
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
