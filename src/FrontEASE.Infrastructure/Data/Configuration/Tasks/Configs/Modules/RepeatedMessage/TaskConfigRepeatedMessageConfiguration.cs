using FrontEASE.Domain.Entities.Tasks.Configs;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.RepeatedMessage
{
    public class TaskConfigRepeatedMessageConfiguration : IEntityTypeConfiguration<TaskConfigRepeatedMessage>
    {
        public void Configure(EntityTypeBuilder<TaskConfigRepeatedMessage> builder)
        {
            new EntityBaseConfiguration<TaskConfigRepeatedMessage>().Configure(builder);
            builder.ToTable(TableConstants.TaskConfigRepeatedMessages, SchemaConstants.Data);

            builder.HasOne(e => e.TaskConfig)
                .WithOne(e => e.RepeatedMessage)
                .HasForeignKey<TaskConfig>(e => e.RepeatedMessageID);

            builder.HasMany(e => e.RepeatedMessageItems)
                .WithOne(e => e.RepeatedMessage)
                .HasForeignKey(e => e.RepeatedMessageID);
        }
    }
}
