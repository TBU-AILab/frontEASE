using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(TableConstants.Users, SchemaConstants.Auth);

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Image)
                .WithOne();

            builder.HasOne(e => e.UserPreferences)
                .WithOne(e => e.User);

            builder.HasOne(e => e.UserRole)
                .WithOne()
                .HasForeignKey<IdentityUserRole<string>>(e => e.UserId);

            builder.HasMany(e => e.Companies)
                .WithMany(e => e.Users);

            builder.HasMany(e => e.Tasks)
                .WithMany(e => e.Members);
        }
    }
}
