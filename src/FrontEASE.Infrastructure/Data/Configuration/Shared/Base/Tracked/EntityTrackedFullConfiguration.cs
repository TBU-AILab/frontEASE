using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Domain.Entities.Base.Tracked;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked
{
    public class EntityTrackedFullConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityTrackedFull
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            new EntityTrackedBaseConfiguration<TEntity>().Configure(builder);

            builder.Property(e => e.DateUpdated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql(SQLCommandConstants.GenerateTimestamp);
        }
    }
}
