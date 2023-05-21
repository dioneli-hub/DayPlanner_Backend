using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.DataAccess.Configurations
{
    public class TaskPerformerConfiguration : IEntityTypeConfiguration<TaskPerformer>
    {
        public void Configure(EntityTypeBuilder<TaskPerformer> builder)
        {
            builder.HasKey(x => new
            {
                x.TaskId,
                x.PerformerId
            });

            builder.HasOne(x => x.Performer)
                .WithMany(x => x.TaskAssignments)
                .HasForeignKey(x => x.PerformerId);

            builder.ToTable("TaskPerformers");
        }
    }
}
