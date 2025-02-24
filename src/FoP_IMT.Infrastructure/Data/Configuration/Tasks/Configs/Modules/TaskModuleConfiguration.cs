using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs.Modules
{
    public class TaskModuleConfiguration : IEntityTypeConfiguration<TaskModuleEntity>
    {
        public void Configure(EntityTypeBuilder<TaskModuleEntity> builder)
        {
            new EntityBaseConfiguration<TaskModuleEntity>().Configure(builder);

            builder.ToTable(TableConstants.TaskModules, SchemaConstants.Data);

            builder.HasMany(e => e.Parameters)
                .WithOne(p => p.Module)
                .HasForeignKey(p => p.ModuleID);

            builder.HasOne(e => e.TaskConfig)
                .WithMany(e => e.Modules)
                .HasForeignKey(e => e.TaskConfigID);

            builder.HasOne(p => p.ParameterEnumValue)
                .WithOne(m => m.ModuleValue)
                .HasForeignKey<TaskModuleParameterEnumValueEntity>(p => p.ModuleValueID);
        }
    }
}
