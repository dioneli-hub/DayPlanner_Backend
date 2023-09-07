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
        public DbSet<RecurringPattern> RecurringPatterns { get; set; }
        public DbSet<BoardMembershipInvitation> BoardMembershipInvitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new BoardConfiguration().Configure(modelBuilder.Entity<Board>());
            new TaskItemConfiguration().Configure(modelBuilder.Entity<TaskItem>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new BoardMemberConfiguration().Configure(modelBuilder.Entity<BoardMember>());
            new NotificationConfiguration().Configure(modelBuilder.Entity<Notification>());
            new RecurringPatternConfiguration().Configure(modelBuilder.Entity<RecurringPattern>());
            new BoardMembershipInvitationConfiguration().Configure(modelBuilder.Entity<BoardMembershipInvitation>());



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
                    FirstName = "Madison",
                    LastName = "Walker",
                    Email = "Dioneli@mail.ru1",
                    CreatedAt = new DateTime(2020, 05, 09, 9, 15, 0),
                    PasswordHash = "x/5fpi8JiMGXxM4Re4fzlamU61mQQMGNR50wxtwCaHw=",
                    SaltHash = "mlJyHV/cYHAT2ErFkB8d5w==",
                    VerificationToken = "bCGM/xNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==",
                    VerifiedAt = new DateTime(2020, 05, 09, 9, 15, 0)
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
                    SaltHash = "FyQp6hr65+F7jI0btRXMLw==",
                    VerificationToken = "OCGOOxNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==",
                    VerifiedAt = new DateTime(2020, 05, 09, 9, 15, 0)
                });

            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 3,
                   FirstName = "Viktor",
                   LastName = "Dimashevski",
                   Email = "vikdim@madeup.mail.com",
                   CreatedAt = new DateTime(2020, 05, 09, 9, 15, 0),
                   PasswordHash = "FBHiJLzMEWDHoMgTd1rqQQbDaucEQStWzFba3FRL54I=",
                   SaltHash = "FyQp6hr65+F7jI0btRXMLw==",
                   VerificationToken = "bCGM/xNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==",
                   VerifiedAt = new DateTime(2020, 05, 09, 9, 15, 0)
               });
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 4,
                   FirstName = "Liam",
                   LastName = "Wall",
                   Email = "liamwall@madeup.mail.com",
                   CreatedAt = new DateTime(2020, 05, 09, 9, 15, 0),
                   PasswordHash = "FBHiJLzMEWDHoMgTd1rqQQbDaucEQStWzFba3FRL54I=",
                   SaltHash = "FyQp6hr65+F7jI0btRXMLw==",
                   VerificationToken = "bCGM/xNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==",
                   VerifiedAt = new DateTime(2020, 05, 09, 9, 15, 0)
               });
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 5,
                   FirstName = "Karen",
                   LastName = "Tailor",
                   Email = "kktailor@madeup.mail.com",
                   CreatedAt = new DateTime(2020, 05, 09, 9, 15, 0),
                   PasswordHash = "FBHiJLzMEWDHoMgTd1rqQQbDaucEQStWzFba3FRL54I=",
                   SaltHash = "FyQp6hr65+F7jI0btRXMLw==",
                   VerificationToken = "bCGM/xNYBYG1jzN5UmkSDY7YqpU8UovU+xz3OP+JlQJS9t0lrW3LTA+lze+KeOvbYXptDmbIDptUcz9L+YeuUg==",
                   VerifiedAt = new DateTime(2020, 05, 09, 9, 15, 0)
               });
        }
    }
}
