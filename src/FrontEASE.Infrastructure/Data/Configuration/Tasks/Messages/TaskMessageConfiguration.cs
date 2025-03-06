using FrontEASE.Domain.Entities.Tasks.Messages;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Runs
{
    public class TaskMessageConfiguration : IEntityTypeConfiguration<TaskMessage>
    {
        public void Configure(EntityTypeBuilder<TaskMessage> builder)
        {
            new EntityTrackedBaseManualStampConfiguration<TaskMessage>().Configure(builder);
            builder.ToTable(TableConstants.TaskMessages, SchemaConstants.Data);

            builder.HasOne(e => e.Task)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.TaskID);

            builder.HasOne(e => e.TaskSolution)
                .WithOne(e => e.TaskMessage)
                .HasForeignKey<TaskSolution>(e => e.TaskMessageID);

            builder.HasIndex(e => e.DateCreated).HasDatabaseName($"IX_{TableConstants.TaskMessages}_{nameof(TaskMessage.DateCreated)}");
        }
    }
}
