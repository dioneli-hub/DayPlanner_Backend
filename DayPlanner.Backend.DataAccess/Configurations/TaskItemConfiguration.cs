using DayPlanner.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.DataAccess.Configurations
{
    internal class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.DueDate).IsRequired(); 
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasOne(x => x.Board)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.BoardId);

            
            builder.ToTable("TaskItems");
        }

    }
}
