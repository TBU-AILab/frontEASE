using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values.List
{
    public class TaskModuleParameterListValueConfiguration : IEntityTypeConfiguration<TaskModuleParameterListValueEntity>
    {
        public void Configure(EntityTypeBuilder<TaskModuleParameterListValueEntity> builder)
        {
            new EntityBaseConfiguration<TaskModuleParameterListValueEntity>().Configure(builder);

            builder.ToTable(TableConstants.TaskModuleParameterListValues, SchemaConstants.Data);

            builder.HasOne(p => p.ParameterValue)
                .WithOne(m => m.ListValue)
                .HasForeignKey<TaskModuleParameterValueEntity>(p => p.ListValueID);

            builder.HasMany(e => e.ParameterValues)
                .WithOne(e => e.ListParamValue)
                .HasForeignKey(e => e.ListParamValueID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
