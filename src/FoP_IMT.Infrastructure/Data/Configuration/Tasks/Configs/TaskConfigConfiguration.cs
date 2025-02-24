using FoP_IMT.Domain.Entities.Tasks.Configs;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Tasks.Configs
{
    public class TaskConfigConfiguration : IEntityTypeConfiguration<TaskConfig>
    {
        public void Configure(EntityTypeBuilder<TaskConfig> builder)
        {
            new EntityBaseConfiguration<TaskConfig>().Configure(builder);
            builder.ToTable(TableConstants.TaskConfigs, SchemaConstants.Data);

            builder.HasOne(e => e.Task)
               .WithOne(e => e.Config)
               .HasForeignKey<Domain.Entities.Tasks.Task>(e => e.ConfigID);

            builder.HasOne(e => e.RepeatedMessage)
                .WithOne(e => e.TaskConfig);

            builder.HasMany(e => e.Modules)
                .WithOne(e => e.TaskConfig);

            builder.Ignore(e => e.AvailableModules);
        }
    }
}
