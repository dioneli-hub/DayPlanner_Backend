
namespace DayPlanner.Backend.Domain
{
    public class Notification
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
