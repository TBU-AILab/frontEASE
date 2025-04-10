using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Companies
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            new EntityTrackedFullConfiguration<Company>().Configure(builder);
            builder.ToTable(TableConstants.Companies, SchemaConstants.Auth);

            builder.HasIndex(e => e.IsDeleted)
                .HasDatabaseName($"IX_{nameof(Company)}_{nameof(Company.IsDeleted)}");

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
