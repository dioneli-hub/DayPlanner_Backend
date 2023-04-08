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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .HasData(
                new Board
                {
                    Id = 1,
                    Name = "Test"
                },

                new Board
                {
                    Id = 2,
                    Name = "Test2"
                }
                );

            modelBuilder.Entity<TaskItem>()
                .HasData(
                new TaskItem
                {
                    Id = 1,
                    Text = "Task1",
                    DueDate = new DateTime(2015, 7, 20),
                    CreatedAt = new DateTime(2014, 7, 20),
                    BoardId = 1
                },

                new TaskItem
                {
                    Id = 2,
                    Text = "Task2",
                    DueDate = new DateTime(2015, 7, 20),
                    CreatedAt = new DateTime(2014, 7, 20),
                    BoardId = 1
                },

                new TaskItem
                {
                    Id = 3,
                    Text = "Task3",
                    DueDate = new DateTime(2023, 4, 1),
                    CreatedAt = new DateTime(2014, 7, 20),
                    BoardId = 1
                },

                new TaskItem
                {
                    Id = 4,
                    Text = "Task4",
                    DueDate = new DateTime(2023, 4, 1),
                    CreatedAt = new DateTime(2014, 7, 20),
                    BoardId = 1
                }

                );
        }


    }
}
