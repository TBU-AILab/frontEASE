using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Manual;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Tasks.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Tasks.Task> builder)
        {
            new EntityTrackedFullManualStampConfiguration<Domain.Entities.Tasks.Task>().Configure(builder);
            builder.ToTable(TableConstants.Tasks, SchemaConstants.Data);

            builder.HasIndex(e => e.IsDeleted)
                .HasDatabaseName($"IX_{nameof(Domain.Entities.Tasks.Task)}_{nameof(Domain.Entities.Tasks.Task.IsDeleted)}");
            builder.HasIndex(e => e.State)
                .HasDatabaseName($"IX_{nameof(Domain.Entities.Tasks.Task)}_{nameof(Domain.Entities.Tasks.Task.State)}");
            builder.HasIndex(e => e.DateCreated)
                .HasDatabaseName($"IX_{nameof(Domain.Entities.Tasks.Task)}_{nameof(Domain.Entities.Tasks.Task.DateCreated)}");
            builder.HasIndex(e => e.DateUpdated)
                .HasDatabaseName($"IX_{nameof(Domain.Entities.Tasks.Task)}_{nameof(Domain.Entities.Tasks.Task.DateUpdated)}");

            builder.HasIndex(e => new { e.State, e.DateCreated })
                .HasDatabaseName($"IX_{nameof(Domain.Entities.Tasks.Task)}_{nameof(Domain.Entities.Tasks.Task.State)}_{nameof(Domain.Entities.Tasks.Task.DateCreated)}");
            builder.HasIndex(e => new { e.IsDeleted, e.State })
                .HasDatabaseName($"IX_{nameof(Domain.Entities.Tasks.Task)}_{nameof(Domain.Entities.Tasks.Task.IsDeleted)}_{nameof(Domain.Entities.Tasks.Task.State)}");

            builder.HasOne(e => e.Config)
                .WithOne(e => e.Task);

            builder.HasMany(e => e.Solutions)
                .WithOne(e => e.Task)
                .HasForeignKey(e => e.TaskID);

            builder.HasMany(e => e.Messages)
                .WithOne(e => e.Task)
                .HasForeignKey(e => e.TaskID);

            builder.HasMany(e => e.Members)
                .WithMany(e => e.Tasks);

            builder.HasMany(e => e.MemberGroups)
                .WithMany(e => e.Tasks);
        }
    }
}
