using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked
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
