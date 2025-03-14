using FrontEASE.Domain.Entities.Base.Manual;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Base
{
    public class EntityBaseManualStampConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBaseManualStamp
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(e => e.ID)
                .ValueGeneratedNever();
        }
    }
}
