using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values.List
{
    public class TaskModuleParameterListValueItemConfiguration : IEntityTypeConfiguration<TaskModuleParameterListValueItemEntity>
    {
        public void Configure(EntityTypeBuilder<TaskModuleParameterListValueItemEntity> builder)
        {
            new EntityBaseConfiguration<TaskModuleParameterListValueItemEntity>().Configure(builder);

            builder.ToTable(TableConstants.TaskModuleParameterListValueItems, SchemaConstants.Data);

            builder.HasOne(p => p.ListParamValue)
                .WithMany(m => m.ParameterValues);

            builder.HasMany(e => e.ParameterItems)
                .WithOne(e => e.ListValue)
                .HasForeignKey(e => e.ListValueID);
        }
    }
}
