namespace DayPlanner.Backend.DataAccess.Entities
{
    public class Board
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
