using FoP_IMT.Domain.Entities.Base.Manual;
using FoP_IMT.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked
{
    public class EntityTrackedFullManualStampConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityTrackedFullManualStamp
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            new EntityTrackedBaseManualStampConfiguration<TEntity>().Configure(builder);

            builder.Property(e => e.DateUpdated)
                .HasDefaultValueSql(SQLCommandConstants.GenerateTimestamp);
        }
    }
}
