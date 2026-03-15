using FrontEASE.Domain.Entities.Management.General.Columns;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Management.Modules.GeneralOptions
{
    public class UserPreferenceGeneralOptionTaskGridColumnConfiguration : IEntityTypeConfiguration<UserPreferenceGeneralOptionTaskGridColumn>
    {
        public void Configure(EntityTypeBuilder<UserPreferenceGeneralOptionTaskGridColumn> builder)
        {
            new EntityBaseConfiguration<UserPreferenceGeneralOptionTaskGridColumn>().Configure(builder);
            builder.ToTable(TableConstants.UserPreferenceTaskDataGridColumns, SchemaConstants.Data);

            builder.HasOne(e => e.GeneralOptions)
                .WithMany(e => e.TaskGridColumns)
                .HasForeignKey(e => e.GeneralOptionsID);
        }
    }
}