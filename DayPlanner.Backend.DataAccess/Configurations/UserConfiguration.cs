using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.SaltHash).IsRequired();

            //builder.HasOne(x => x.Avatar)
            //    .WithMany()
            //    .HasForeignKey(x => x.AvatarFileId);

            builder.ToTable("Users");
        }

    }
}
