using FoP_IMT.Domain.Entities.Management.Tokens.Connectors;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Management.Modules.Connectors
{
    public class UserPreferenceTokenOptionConnectorTypeConfiguration : IEntityTypeConfiguration<UserPreferenceTokenOptionConnectorType>
    {
        public void Configure(EntityTypeBuilder<UserPreferenceTokenOptionConnectorType> builder)
        {
            new EntityBaseConfiguration<UserPreferenceTokenOptionConnectorType>().Configure(builder);
            builder.ToTable(TableConstants.UserPreferenceTokenConnectorTypes, SchemaConstants.Data);

            builder.HasOne(e => e.TokenOption)
                .WithMany(e => e.ConnectorTypes)
                .HasForeignKey(e => e.TokenOptionID);
        }
    }
}
