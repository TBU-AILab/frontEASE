using FoP_IMT.Domain.Entities.Companies;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Companies
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            new EntityTrackedFullConfiguration<Company>().Configure(builder);
            builder.ToTable(TableConstants.Companies, SchemaConstants.Auth);

            builder.HasOne(e => e.Address)
                .WithOne();

            builder.HasOne(e => e.Image)
                .WithOne();

            builder.HasMany(e => e.Users)
                .WithMany(e => e.Companies);

            builder.HasMany(e => e.Tasks)
                .WithMany(e => e.MemberGroups);
        }
    }
}
