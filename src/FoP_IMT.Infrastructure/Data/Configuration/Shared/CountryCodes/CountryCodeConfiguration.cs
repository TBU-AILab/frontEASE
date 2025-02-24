using FoP_IMT.Domain.Entities.Shared.CountryCodes;
using FoP_IMT.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.CountryCodes
{
    public class CountryCodeConfiguration : IEntityTypeConfiguration<CountryCode>
    {
        public void Configure(EntityTypeBuilder<CountryCode> builder)
        {
            builder.ToTable(TableConstants.CountryCodes, SchemaConstants.App);

            builder.HasMany(e => e.Resources)
                    .WithOne(e => e.CountryCode)
                    .HasForeignKey(e => e.CountryCodeID);
        }
    }
}
