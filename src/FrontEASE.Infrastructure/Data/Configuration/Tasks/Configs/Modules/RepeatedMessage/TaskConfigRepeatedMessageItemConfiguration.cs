using FrontEASE.Domain.Entities.Tasks.Configs.Modules.RepeatedMessage;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Configs.Modules.RepeatedMessage
{
    public class TaskConfigRepeatedMessageItemConfiguration : IEntityTypeConfiguration<TaskConfigRepeatedMessageItem>
    {
        public void Configure(EntityTypeBuilder<TaskConfigRepeatedMessageItem> builder)
        {
            new EntityBaseConfiguration<TaskConfigRepeatedMessageItem>().Configure(builder);
            builder.ToTable(TableConstants.TaskConfigRepeatedMessageItems, SchemaConstants.Data);

            builder.HasOne(e => e.RepeatedMessage)
                .WithMany(e => e.RepeatedMessageItems)
                .HasForeignKey(e => e.RepeatedMessageID);
        }
    }
}
