using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options;

namespace FoP_IMT.Domain.Entities.Tasks.Configs
{
    public class TaskConfig : EntityBase
    {
        public TaskConfig()
        {
            Task = null!;
            RepeatedMessage = new();

            Modules = [];
            AvailableModules = [];

            Name = string.Empty;
            InitMessage = string.Empty;
        }

        #region Navigation

        public Task Task { get; set; }

        public Guid RepeatedMessageID { get; set; }
        public TaskConfigRepeatedMessage RepeatedMessage { get; set; }

        public IList<TaskModuleEntity> Modules { get; set; }
        public IList<TaskModule> AvailableModules { get; set; }

        #endregion

        #region Data

        public string Name { get; set; }
        public bool FeedbackFromSolution { get; set; }
        public int MaxContextSize { get; set; }
        public string? SystemMessage { get; set; }
        public string InitMessage { get; set; }

        #endregion
    }
}
