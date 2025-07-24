using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired();

            builder.Property(u => u.LastName).HasMaxLength(100).IsRequired();

            builder.Property(u => u.Status).HasConversion<int>().HasDefaultValue(Domain.Enums.UserStatus.Active);
        }
    }
}