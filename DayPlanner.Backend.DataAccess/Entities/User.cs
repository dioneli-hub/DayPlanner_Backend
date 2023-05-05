using System;
using System.Collections.Generic;
using System.Linq;

namespace DayPlanner.Backend.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string PasswordHash { get; set; }
        public string SaltHash { get; set; }

        //public int? AvatarFileId { get; set; }
        //public ApplicationFile Avatar { get; set; }
        public ICollection<Board> Boards { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
    }
}
