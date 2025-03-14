using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs
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
