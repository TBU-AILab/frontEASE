using FoP_IMT.Domain.Entities.Management;
using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Management
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
