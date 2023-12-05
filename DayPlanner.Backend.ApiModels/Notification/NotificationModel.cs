namespace DayPlanner.Backend.ApiModels
{
    public class NotificationModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }
    }
}
