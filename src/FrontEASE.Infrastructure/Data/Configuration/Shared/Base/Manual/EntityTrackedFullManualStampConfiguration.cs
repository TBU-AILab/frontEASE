using FrontEASE.Domain.Entities.Base.Manual;
using FrontEASE.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked
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
