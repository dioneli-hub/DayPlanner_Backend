namespace DayPlanner.Backend.Api.ApiModels
{
    public class BoardModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatorId { get; set; }

    }
}
