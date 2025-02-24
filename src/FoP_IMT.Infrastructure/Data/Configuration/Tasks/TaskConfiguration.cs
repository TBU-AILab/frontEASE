using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base.Tracked;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoP_IMT.Infrastructure.Data.Configuration.Tasks
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Tasks.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Tasks.Task> builder)
        {
            new EntityTrackedFullManualStampConfiguration<Domain.Entities.Tasks.Task>().Configure(builder);
            builder.ToTable(TableConstants.Tasks, SchemaConstants.Data);

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
