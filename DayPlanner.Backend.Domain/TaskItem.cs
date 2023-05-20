namespace DayPlanner.Backend.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTimeOffset DueDate { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public int CreatorId { get; set; }

        public User Creator { get; set; }  //make not null later???

        //public int PerformerId { get; set; }

        //public User Performer { get; set; }  

        public int BoardId { get; set; }

        public Board Board { get; set; }
    }
}
