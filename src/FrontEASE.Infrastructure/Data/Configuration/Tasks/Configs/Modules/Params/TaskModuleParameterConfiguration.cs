using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params
{
    public class TaskModuleParameterConfiguration : IEntityTypeConfiguration<TaskModuleParameterEntity>
    {
        public void Configure(EntityTypeBuilder<TaskModuleParameterEntity> builder)
        {
            new EntityBaseConfiguration<TaskModuleParameterEntity>().Configure(builder);

            builder.ToTable(TableConstants.TaskModuleParameters, SchemaConstants.Data);

            builder.HasOne(p => p.Module)
                .WithMany(m => m.Parameters)
                .HasForeignKey(p => p.ModuleID);

            builder.HasOne(e => e.Value)
                .WithOne(e => e.Parameter);

            builder.HasOne(e => e.ListValue)
                .WithMany(e => e.ParameterItems)
                .HasForeignKey(p => p.ListValueID);
        }
    }
}
