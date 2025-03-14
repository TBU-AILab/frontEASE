using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.General;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Management.Modules
{
    public class UserPreferenceGeneralOptionsConfiguration : IEntityTypeConfiguration<UserPreferenceGeneralOptions>
    {
        public void Configure(EntityTypeBuilder<UserPreferenceGeneralOptions> builder)
        {
            new EntityBaseConfiguration<UserPreferenceGeneralOptions>().Configure(builder);
            builder.ToTable(TableConstants.UserPreferenceGeneralOptions, SchemaConstants.Data);

            builder.HasOne(e => e.UserPreferences)
               .WithOne(e => e.GeneralOptions)
               .HasForeignKey<UserPreferences>(e => e.GeneralOptionsID);
        }
    }
}
