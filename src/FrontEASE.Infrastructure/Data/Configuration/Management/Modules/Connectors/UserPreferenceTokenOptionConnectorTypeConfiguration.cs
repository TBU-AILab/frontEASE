using FrontEASE.Domain.Entities.Management.Tokens.Connectors;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Management.Modules.Connectors
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