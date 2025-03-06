using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked
{
    public class EntityTrackedBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityTrackedBase
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            new EntityBaseConfiguration<TEntity>().Configure(builder);

            builder.Property(e => e.DateCreated)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql(SQLCommandConstants.GenerateTimestamp);

            builder.Property(e => e.DateCreated).Metadata
                .SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Throw);
        }
    }
}
