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

        }


    }
}
