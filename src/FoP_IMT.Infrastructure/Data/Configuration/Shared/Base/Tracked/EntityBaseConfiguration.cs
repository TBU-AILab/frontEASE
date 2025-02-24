using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked
{
    public class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(e => e.ID)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql(SQLCommandConstants.GenerateUIDCommand);
        }
    }
}
