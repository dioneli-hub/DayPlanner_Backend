using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.DataAccess.Configurations
{
    internal class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();

            builder.ToTable("Boards");
        }
    
    }
}
