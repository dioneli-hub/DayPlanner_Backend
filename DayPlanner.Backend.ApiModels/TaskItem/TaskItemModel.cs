using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.ApiModels.User;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.ApiModels.TaskItem
{
    public class TaskItemModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTimeOffset DueDate { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public int BoardId { get; set; }
        public BoardModel Board { get; set; }

        public int CreatorId { get; set; }
        public UserModel Creator { get; set; } 

     }
}
