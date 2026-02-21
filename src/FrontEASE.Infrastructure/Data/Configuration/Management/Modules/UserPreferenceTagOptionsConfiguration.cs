using FrontEASE.Domain.Entities.Management.Tags;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Management.Modules
{
    public class UserPreferenceTagOptionsConfiguration : IEntityTypeConfiguration<UserPreferenceTagOption>
    {
        public void Configure(EntityTypeBuilder<UserPreferenceTagOption> builder)
        {
            new EntityBaseConfiguration<UserPreferenceTagOption>().Configure(builder);
            builder.ToTable(TableConstants.UserPreferenceTags, SchemaConstants.Data);

            builder.HasOne(e => e.UserPreferences)
                .WithMany(e => e.TagOptions)
                .HasForeignKey(e => e.UserPreferencesID);

            builder.HasMany(e => e.Tasks)
                .WithMany(e => e.Tags);
        }
    }
}
