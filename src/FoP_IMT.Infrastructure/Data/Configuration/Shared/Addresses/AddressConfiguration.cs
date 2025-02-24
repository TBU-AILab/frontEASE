using FoP_IMT.Domain.Entities.Shared.Addresses;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.Addresses
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            new EntityTrackedFullConfiguration<Address>().Configure(builder);
            builder.ToTable(TableConstants.Addresses, SchemaConstants.Data);
        }
    }
}
