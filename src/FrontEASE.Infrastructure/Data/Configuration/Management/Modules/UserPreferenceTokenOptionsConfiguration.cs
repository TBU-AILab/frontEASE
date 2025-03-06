using FrontEASE.Domain.Entities.Management.Tokens;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Management.Modules
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