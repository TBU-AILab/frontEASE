using FrontEASE.Domain.Entities.Shared.Resources;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Shared.Data.Enums.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Resources
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable(TableConstants.Resources, SchemaConstants.App);
            builder.HasKey(e => e.ResourceCode);

            builder.HasIndex(e => new { e.CountryCodeID, e.ResourceCode })
                .HasDatabaseName($"IX_{nameof(Resource)}_{nameof(Resource.CountryCodeID)}_{nameof(Resource.ResourceCode)}");

            builder.HasOne(e => e.CountryCode)
                .WithMany(e => e.Resources)
                .HasForeignKey(e => e.CountryCodeID);

            builder.Property(e => e.CountryCodeID)
               .HasDefaultValue(LanguageCode.EN);
        }
    }
}
