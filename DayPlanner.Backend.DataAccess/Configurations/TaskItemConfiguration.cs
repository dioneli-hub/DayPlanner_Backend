﻿using DayPlanner.Backend.Domain;
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
            builder.Property(x => x.IsCompleted).IsRequired();
            builder.Property(x => x.ChangeRecurredChildren).IsRequired();

            builder.HasOne(x => x.Board)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.BoardId)
                .OnDelete(DeleteBehavior.NoAction); ;

            builder.HasOne(x => x.Creator)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.NoAction); ;

            builder.HasOne(x => x.Performer) //to check if right
                .WithMany()
                .HasForeignKey(x => x.PerformerId);

            builder.ToTable("TaskItems");
        }

    }
}
