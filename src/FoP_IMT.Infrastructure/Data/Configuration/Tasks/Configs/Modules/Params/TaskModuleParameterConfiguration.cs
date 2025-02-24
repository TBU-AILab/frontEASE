using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs.Modules.Params
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
        }
    }
}
