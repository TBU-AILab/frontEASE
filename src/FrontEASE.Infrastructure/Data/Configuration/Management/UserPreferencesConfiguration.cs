using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Management
{
    public class UserPreferencesConfiguration : IEntityTypeConfiguration<UserPreferences>
    {
        public void Configure(EntityTypeBuilder<UserPreferences> builder)
        {
            new EntityBaseConfiguration<UserPreferences>().Configure(builder);
            builder.ToTable(TableConstants.UserPreferences, SchemaConstants.Data);

            builder.HasOne(e => e.User)
               .WithOne(e => e.UserPreferences)
               .HasForeignKey<ApplicationUser>(e => e.UserPreferencesID);

            builder.HasMany(e => e.TokenOptions)
                .WithOne(e => e.UserPreferences);

            builder.HasOne(e => e.GeneralOptions)
                .WithOne(e => e.UserPreferences);
        }
    }
}
