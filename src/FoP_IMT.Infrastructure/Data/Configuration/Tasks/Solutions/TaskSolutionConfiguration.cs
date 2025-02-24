using FoP_IMT.Domain.Entities.Tasks.Shared;
using FoP_IMT.Domain.Entities.Tasks.Solutions;
using FoP_IMT.Infrastructure.Constants;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace FoP_IMT.Infrastructure.Data.Configuration.Tasks.Runs
{
    public class TaskSolutionConfiguration : IEntityTypeConfiguration<TaskSolution>
    {
        public void Configure(EntityTypeBuilder<TaskSolution> builder)
        {
            new EntityBaseManualStampConfiguration<TaskSolution>().Configure(builder);
            builder.ToTable(TableConstants.TaskSolutions, SchemaConstants.Data);

            builder.HasOne(e => e.Task)
                .WithMany(e => e.Solutions)
                .HasForeignKey(e => e.TaskID);

            builder.HasOne(e => e.TaskMessage)
                .WithOne(e => e.TaskSolution);

            builder.Property(e => e.Metadata)
                .HasConversion(
                    v => JsonSerializer.Serialize(v.ToDictionary(item => item.Key, item => item.Value), default(JsonSerializerOptions)),
                    v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, default(JsonSerializerOptions))!
                                .Select(kv => new TaskKeyValueItem { Key = kv.Key, Value = kv.Value })
                                .ToList() ?? new List<TaskKeyValueItem>()
                );
        }
    }
}
