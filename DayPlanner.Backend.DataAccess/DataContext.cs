using DayPlanner.Backend.DataAccess.Configurations;
using DayPlanner.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new BoardConfiguration().Configure(modelBuilder.Entity<Board>());
            new TaskItemConfiguration().Configure(modelBuilder.Entity<TaskItem>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Henry",
                    LastName = "Moor",
                    Email = "hmoor@gmail.com",
                    CreatedAt = new DateTime(2019, 05, 09, 9, 15, 0)
                });

            //modelBuilder.Entity<TaskItem>().HasData(
            //    new TaskItem
            //    {
            //        Id = 1,
            //        BoardId = 2,
            //        Text = "Jog 5km",
            //        DueDate = new DateTime(2023, 04, 10, 0, 0, 0),
            //        CreatedAt = new DateTime(2020, 05, 09, 9, 15, 0),
            //        CreatorId = 1
            //    });
        }
    }
}
