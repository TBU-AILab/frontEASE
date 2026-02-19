using FrontEASE.Domain.Entities.Tasks.Logs;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Manual;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Logs
{
    public class TaskLogConfiguration : IEntityTypeConfiguration<TaskLog>
    {
        public void Configure(EntityTypeBuilder<TaskLog> builder)
        {
            new EntityBaseManualStampConfiguration<TaskLog>().Configure(builder);
            builder.ToTable(TableConstants.TaskLogs, SchemaConstants.Data);

            builder.HasOne(e => e.Task)
                .WithMany(e => e.Logs)
                .HasForeignKey(e => e.TaskID);
        }
    }
}
