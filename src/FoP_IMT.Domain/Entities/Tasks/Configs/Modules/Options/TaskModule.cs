using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FoP_IMT.Shared.Data.Enums.Tasks.Config;

namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options
{
    public class TaskModule : ITaskModuleBase
    {
        public TaskModule()
        {
            ShortName = string.Empty;
            LongName = string.Empty;
            Description = string.Empty;

            Parameters = [];
        }

        #region Navigation

        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Description { get; set; }
        public ModuleType PackageType { get; set; }
        public IList<TaskModuleParameter> Parameters { get; set; }

        #endregion
    }
}
