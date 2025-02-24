using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values
{
    public class TaskModuleParameterValueConfiguration : IEntityTypeConfiguration<TaskModuleParameterValueEntity>
    {
        public void Configure(EntityTypeBuilder<TaskModuleParameterValueEntity> builder)
        {
            new EntityBaseConfiguration<TaskModuleParameterValueEntity>().Configure(builder);

            builder.ToTable(TableConstants.TaskModuleParameterValues, SchemaConstants.Data);

            builder.HasOne(p => p.Parameter)
                .WithOne(m => m.Value)
                .HasForeignKey<TaskModuleParameterEntity>(p => p.ValueID);

            builder.HasOne(e => e.EnumValue)
                .WithOne(e => e.ParameterValue);
        }
    }
}
