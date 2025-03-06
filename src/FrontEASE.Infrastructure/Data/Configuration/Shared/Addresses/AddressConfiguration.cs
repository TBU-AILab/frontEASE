using FrontEASE.Domain.Entities.Shared.Addresses;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Addresses
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
