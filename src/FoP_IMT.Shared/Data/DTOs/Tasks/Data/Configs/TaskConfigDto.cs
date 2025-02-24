using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FoP_IMT.Shared.Data.Enums.Tasks.Config;
using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs
{
    /// <summary>
    /// DTO for task configuration and settings
    /// </summary>
    public class TaskConfigDto
    {
        public TaskConfigDto()
        {
            Connector = new() { PackageType = ModuleType.LLM_CONNECTOR };
            Evaluator = new() { PackageType = ModuleType.EVALUATOR };
            Solution = new() { PackageType = ModuleType.SOLUTION };

            RepeatedMessage = new();

            Tests = [];
            StoppingConditions = [];
            Analyses = [];
            Stats = [];
            AvailableModules = [];

            Name = string.Empty;
            InitMessage = string.Empty;
            SystemMessage = string.Empty;

            MaxContextSize = 100;
        }

        #region Navigation

        /// <summary>
        /// Preset - repeated (iterative) user message
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(RepeatedMessage)}")]
        [RequiredValidation<TaskConfigDto>]
        public TaskConfigRepeatedMessageDto RepeatedMessage { get; set; }

        /// <summary>
        /// Task LLM connector
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(Connector)}")]
        [RequiredValidation<TaskConfigDto>]
        public TaskModuleDto Connector { get; set; }

        /// <summary>
        /// Task solution type
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(Solution)}")]
        [RequiredValidation<TaskConfigDto>]
        public TaskModuleDto Solution { get; set; }

        /// <summary>
        /// Task solution evaluator
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(Evaluator)}")]
        [RequiredValidation<TaskConfigDto>]
        public TaskModuleDto Evaluator { get; set; }

        /// <summary>
        /// Quality-measurement tests executed to validate solution
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(Tests)}")]
        [RequiredValidation<TaskConfigDto>]
        public IList<TaskModuleDto> Tests { get; set; }

        /// <summary>
        /// Stopping conditions applied for this task
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(StoppingConditions)}")]
        [RequiredValidation<TaskConfigDto>]
        public IList<TaskModuleDto> StoppingConditions { get; set; }

        /// <summary>
        /// Analyses for this task
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(Analyses)}")]
        [RequiredValidation<TaskConfigDto>]
        public IList<TaskModuleDto> Analyses { get; set; }

        /// <summary>
        /// Stats for this task
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(Stats)}")]
        [RequiredValidation<TaskConfigDto>]
        public IList<TaskModuleDto> Stats { get; set; }

        /// <summary>
        /// Available modules for this task
        /// </summary>
        public IList<TaskModuleNoValidationDto> AvailableModules { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Task name
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(Name)}")]
        [RequiredValidation<TaskConfigDto>]
        [StringLengthValidation(4, 256)]
        public string Name { get; set; }

        /// <summary>
        /// Flag indicating whether feedback from solution is expected
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(FeedbackFromSolution)}")]
        [RequiredValidation<TaskConfigDto>]
        public bool FeedbackFromSolution { get; set; }

        /// <summary>
        /// Preset - maximum size of context
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(MaxContextSize)}")]
        [RequiredValidation<TaskConfigDto>]
        [RangeValidation(-1, 1000)]
        public int MaxContextSize { get; set; }

        /// <summary>
        /// Preset - system message (if available)
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(SystemMessage)}")]
        [RequiredValidation<TaskConfigDto>]
        [StringLengthValidation(16, 8192)]
        public string SystemMessage { get; set; }

        /// <summary>
        /// Preset - initial user message
        /// </summary>
        [Resource($"{nameof(TaskConfigDto)}.{nameof(InitMessage)}")]
        [RequiredValidation<TaskConfigDto>]
        [StringLengthValidation(16, 8192)]
        public string InitMessage { get; set; }

        #endregion
    }
}
