using Microsoft.Identity.Client;

namespace DayPlanner.Backend.DataAccess.Entities
{
    public class Board
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public int CreatorId { get; set; }

        public User Creator { get; set; }  

        public ICollection<TaskItem>? Tasks { get; set; }

    }
}
