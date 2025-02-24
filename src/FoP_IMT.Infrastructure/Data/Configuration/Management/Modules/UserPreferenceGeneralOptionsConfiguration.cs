using FoP_IMT.Domain.Entities.Management;
using FoP_IMT.Domain.Entities.Management.General;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Management.Modules
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
