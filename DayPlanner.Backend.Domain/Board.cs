namespace DayPlanner.Backend.Domain
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<BoardMember> BoardMemberships { get; set; }
        public ICollection<User> BoardMembers { get; set; }

    }
}
