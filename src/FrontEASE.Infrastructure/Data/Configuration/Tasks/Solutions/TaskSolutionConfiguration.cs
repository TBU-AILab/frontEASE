using FrontEASE.Domain.Entities.Tasks.Shared;
using FrontEASE.Domain.Entities.Tasks.Solutions;
using FrontEASE.Infrastructure.Constants;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Base.Manual;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace FrontEASE.Infrastructure.Data.Configuration.Tasks.Solutions
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

            var metadataComparer = new ValueComparer<IList<TaskKeyValueItem>>(
                (l, r) => ReferenceEquals(l, r) || (l != null && r != null && l.Count == r.Count && !l.Where((t, i) => t.Key != r[i].Key || t.Value != r[i].Value).Any()),
                v => v == null ? 0 : v.Aggregate(0, (a, i) => HashCode.Combine(a, i.Key, i.Value)),
                v => v == null ? new List<TaskKeyValueItem>() : v.Select(i => new TaskKeyValueItem { Key = i.Key, Value = i.Value }).ToList()
            );

            builder.Property(e => e.Metadata)
                .HasConversion(
                    v => JsonSerializer.Serialize(v.ToDictionary(item => item.Key, item => item.Value), default(JsonSerializerOptions)),
                    v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, default(JsonSerializerOptions))!
                                .Select(kv => new TaskKeyValueItem { Key = kv.Key, Value = kv.Value })
                                .ToList() ?? new List<TaskKeyValueItem>()
                )
                .Metadata.SetValueComparer(metadataComparer);
        }
    }
}
