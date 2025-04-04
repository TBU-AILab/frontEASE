using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.Enums.Tasks.Config;

namespace FrontEASE.Shared.Data.DTOs.Tasks.UI
{
    /// <summary>
    /// Task state used for evaluation for necessary re-initialisations
    /// </summary>
    public class TaskStateInfoDto
    {
        public TaskStateInfoDto()
        {
            SelectedOptions = new List<(ModuleType, string)>();
        }

        public IList<(ModuleType Type, string Value)> SelectedOptions;

        public override bool Equals(object? obj)
        {
            if (obj is not TaskStateInfoDto other)
                return false;

            var thisOptionsDict = SelectedOptions
                .GroupBy(x => x.Type)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Value).ToList());

            var otherOptionsDict = other.SelectedOptions
                .GroupBy(x => x.Type)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Value).ToList());

            if (thisOptionsDict.Count != otherOptionsDict.Count)
                return false;

            foreach (var kvp in thisOptionsDict)
            {
                if (!otherOptionsDict.TryGetValue(kvp.Key, out var otherValues) || !kvp.Value.SequenceEqual(otherValues))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return SelectedOptions
                .OrderBy(x => x.Type)
                .Aggregate(0, (hash, item) => hash ^ (item.Type.GetHashCode() * 397) ^ item.Value.GetHashCode());
        }
    }


    /// <summary>
    /// View metadata for evaluating additional information related to task
    /// </summary>
    public class TaskViewMetadataDto
    {
        public TaskDto TaskReference { get; set; }
        public TaskStateInfoDto TaskState { get; set; }
        public bool InitializationInProgres { get; set; }
        public bool ReloadInProgress { get; set; }
        public Guid? SelectedMessageID { get; set; }

        public TaskViewMetadataDto(TaskDto taskReference)
        {
            TaskReference = taskReference;
            TaskState = new();
        }

        public TaskStateInfoDto ComposeSelectedOptions()
        {
            var selectedOptions = new List<(ModuleType Type, string Value)>()
            {
                new (ModuleType.LLM_CONNECTOR, TaskReference.Config.Connector.ShortName),
                new (ModuleType.EVALUATOR, TaskReference.Config.Evaluator.ShortName),
                new (ModuleType.SOLUTION, TaskReference.Config.Solution.ShortName)
            };
            foreach (var analysis in TaskReference.Config.Analyses)
            { selectedOptions.Add(new(ModuleType.ANALYSIS, analysis.ShortName)); }
            foreach (var stat in TaskReference.Config.Stats)
            { selectedOptions.Add(new(ModuleType.STATISTIC, stat.ShortName)); }
            foreach (var test in TaskReference.Config.Tests)
            { selectedOptions.Add(new(ModuleType.TEST, test.ShortName)); }
            foreach (var stoppingCondition in TaskReference.Config.StoppingConditions)
            { selectedOptions.Add(new(ModuleType.STOPPING_CONDITION, stoppingCondition.ShortName)); }

            var newState = new TaskStateInfoDto()
            {
                SelectedOptions = selectedOptions
            };

            return newState;
        }

        public bool OptionsReloadNecessary()
        {
            var newState = ComposeSelectedOptions();

            var equal = TaskState.Equals(newState);
            TaskState = newState;

            return !equal;
        }
    }
}
