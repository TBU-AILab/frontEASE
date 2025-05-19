using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params.Values
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

            builder.HasOne(e => e.ListValue)
                .WithOne(e => e.ParameterValue);
        }
    }
}
