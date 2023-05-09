namespace DayPlanner.Backend.ApiModels.TaskItem
{
    public class EditTaskItemModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public int BoardId { get; set; }

    }
}
