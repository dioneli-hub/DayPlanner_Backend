using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.DataAccess.Configurations
{
    public class RecurringPatternConfiguration
    {
        public void Configure(EntityTypeBuilder<RecurringPattern> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.TaskId).IsRequired();
            builder.Property(x => x.RecurringType).IsRequired();
            builder.Property(x => x.OccurencesNumber).IsRequired();


            builder.ToTable("RecurringPatterns");
        }
    }
}
