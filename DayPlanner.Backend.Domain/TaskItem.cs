namespace DayPlanner.Backend.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatorId { get; set; }

        public User? Creator { get; set; }  //make not null later???

        //public int? PerformerId { get; set; }

        //public User? Performer { get; set; }  

        public int BoardId { get; set; }

        public Board? Board { get; set; }
    }
}
