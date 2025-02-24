using FoP_IMT.Domain.Entities.Shared.Resources;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.Resources
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable(TableConstants.Resources, SchemaConstants.App);

            builder.HasKey(e => e.ResourceCode);

            builder.HasOne(e => e.CountryCode)
                .WithMany(e => e.Resources)
                .HasForeignKey(e => e.CountryCodeID);

            builder.Property(e => e.CountryCodeID)
               .HasDefaultValue(LanguageCode.EN);
        }
    }
}
