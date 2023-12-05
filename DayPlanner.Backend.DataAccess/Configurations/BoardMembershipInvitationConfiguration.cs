using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DayPlanner.Backend.DataAccess.Configurations
{

    public class BoardMembershipInvitationConfiguration : IEntityTypeConfiguration<BoardMembershipInvitation>
    {
        public void Configure(EntityTypeBuilder<BoardMembershipInvitation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.InviterId).IsRequired();
            builder.Property(x => x.InvitedPersonEmail).IsRequired();
            builder.Property(x => x.BoardId).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.InvitationToken).IsRequired();

            builder.ToTable("BoardMembershipInvitations");
        }
    }

}
