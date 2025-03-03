using FoP_IMT.Domain.Entities.Management.Tokens;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Management.Modules
{
    public class UserPreferenceTokenOptionsConfiguration : IEntityTypeConfiguration<UserPreferenceTokenOption>
    {
        public void Configure(EntityTypeBuilder<UserPreferenceTokenOption> builder)
        {
            new EntityTrackedBaseConfiguration<UserPreferenceTokenOption>().Configure(builder);
            builder.ToTable(TableConstants.UserPreferenceTokens, SchemaConstants.Data);

            builder.HasOne(e => e.UserPreferences)
                .WithMany(e => e.TokenOptions)
                .HasForeignKey(e => e.UserPreferencesID);

            builder.HasMany(e => e.ConnectorTypes)
                .WithOne(e => e.TokenOption);
        }
    }
}
