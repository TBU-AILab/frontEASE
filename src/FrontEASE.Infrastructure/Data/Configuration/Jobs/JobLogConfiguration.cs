using FrontEASE.Domain.Entities.Jobs;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Jobs
{
    public class JobLogConfiguration : IEntityTypeConfiguration<JobLog>
    {
        public void Configure(EntityTypeBuilder<JobLog> builder)
        {
            new EntityBaseConfiguration<JobLog>().Configure(builder);
            builder.ToTable(TableConstants.JobExecutions, SchemaConstants.App);

            builder.HasIndex(j => new { j.JobName, j.Success, j.DateStart })
                   .HasDatabaseName($"IX_{nameof(JobLog)}_{nameof(JobLog.JobName)}_{nameof(JobLog.Success)}_{nameof(JobLog.DateStart)}");
        }
    }
}
