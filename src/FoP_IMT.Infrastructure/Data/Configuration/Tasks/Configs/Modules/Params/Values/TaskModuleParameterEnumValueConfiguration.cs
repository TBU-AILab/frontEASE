using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values
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
