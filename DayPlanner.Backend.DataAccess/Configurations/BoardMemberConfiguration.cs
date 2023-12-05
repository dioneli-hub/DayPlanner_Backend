using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.DataAccess.Configurations
{
    public class BoardMemberConfiguration : IEntityTypeConfiguration<BoardMember>
    {
        public void Configure(EntityTypeBuilder<BoardMember> builder)
        {
            builder.HasKey(x => new
            {
                x.BoardId,
                x.MemberId
            });

            builder.HasOne(x => x.Board)
                .WithMany(x => x.BoardMemberships)
                .HasForeignKey(x => x.BoardId);

            builder.HasOne(x => x.Member)
                .WithMany(x => x.Memberships)
                .HasForeignKey(x => x.MemberId);

            builder.ToTable("BoardMembers");
        }
    }
}

