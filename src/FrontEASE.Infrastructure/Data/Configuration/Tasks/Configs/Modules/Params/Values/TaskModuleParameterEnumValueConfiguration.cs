using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values
{
    public class TaskModuleParameterEnumValueConfiguration : IEntityTypeConfiguration<TaskModuleParameterEnumValueEntity>
    {
        public void Configure(EntityTypeBuilder<TaskModuleParameterEnumValueEntity> builder)
        {
            new EntityBaseConfiguration<TaskModuleParameterEnumValueEntity>().Configure(builder);

            builder.ToTable(TableConstants.TaskModuleParameterEnumValues, SchemaConstants.Data);

            builder.HasOne(p => p.ParameterValue)
                .WithOne(m => m.EnumValue)
                .HasForeignKey<TaskModuleParameterValueEntity>(p => p.EnumValueID);

            builder.HasOne(e => e.ModuleValue)
                .WithOne(e => e.ParameterEnumValue);
        }
    }
}
