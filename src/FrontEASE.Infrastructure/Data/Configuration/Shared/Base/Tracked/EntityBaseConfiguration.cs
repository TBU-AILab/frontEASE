using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked
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
