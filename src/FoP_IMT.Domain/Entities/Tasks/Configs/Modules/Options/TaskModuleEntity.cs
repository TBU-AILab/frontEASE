using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Shared.Data.Enums.Tasks.Config;

namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options
{
    public class TaskModuleEntity : EntityBase, ITaskModuleBase
    {
        public TaskModuleEntity()
        {
            ShortName = string.Empty;
            Parameters = [];

            TaskConfig = null!;
            ParameterEnumValue = null!;
        }

        #region Navigation

        public Guid TaskConfigID { get; set; }
        public TaskConfig TaskConfig { get; set; }

        public TaskModuleParameterEnumValueEntity ParameterEnumValue { get; set; }

        public IList<TaskModuleParameterEntity> Parameters { get; set; }

        #endregion

        #region Data

        public string ShortName { get; set; }
        public ModuleType PackageType { get; set; }

        #endregion
    }
}
