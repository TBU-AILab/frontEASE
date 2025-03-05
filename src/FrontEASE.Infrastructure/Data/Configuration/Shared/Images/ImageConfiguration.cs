using FrontEASE.Domain.Entities.Shared.Images;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.Images
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            new EntityBaseConfiguration<Image>().Configure(builder);
            builder.ToTable(TableConstants.Images, SchemaConstants.App);

            builder.Ignore(e => e.ImageData);
        }
    }
}
