using FoP_IMT.Domain.Entities.Shared.Images;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.Images
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
