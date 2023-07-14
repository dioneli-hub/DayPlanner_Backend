using DayPlanner.Backend.DataAccess.Configurations;
using DayPlanner.Backend.Domain;
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
        public DbSet<BoardMember> BoardMembers { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new BoardConfiguration().Configure(modelBuilder.Entity<Board>());
            new TaskItemConfiguration().Configure(modelBuilder.Entity<TaskItem>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new BoardMemberConfiguration().Configure(modelBuilder.Entity<BoardMember>());
            new NotificationConfiguration().Configure(modelBuilder.Entity<Notification>());



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

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Di",
                    LastName = "Li",
                    Email = "Dioneli@mail.ru1",
                    CreatedAt = new DateTime(2020, 05, 09, 9, 15, 0),
                    PasswordHash = "x/5fpi8JiMGXxM4Re4fzlamU61mQQMGNR50wxtwCaHw=",
                    SaltHash = "mlJyHV/cYHAT2ErFkB8d5w=="
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    FirstName = "Sam",
                    LastName = "McGregor",
                    Email = "D1!q2222@ru",
                    CreatedAt = new DateTime(2020, 05, 09, 9, 15, 0),
                    PasswordHash = "FBHiJLzMEWDHoMgTd1rqQQbDaucEQStWzFba3FRL54I=",
                    SaltHash = "FyQp6hr65+F7jI0btRXMLw=="
                });
        }
    }
}
