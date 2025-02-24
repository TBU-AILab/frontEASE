using FoP_IMT.Domain.Entities.Base.Manual;
using FoP_IMT.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked
{
    public class EntityTrackedBaseManualStampConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityTrackedBaseManualStamp
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            new EntityBaseManualStampConfiguration<TEntity>().Configure(builder);

            builder.Property(e => e.DateCreated)
                .HasDefaultValueSql(SQLCommandConstants.GenerateTimestamp);
        }
    }
}
