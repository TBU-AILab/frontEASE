using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.Enum;
using FrontEASE.Shared.Data.Enums.Tasks.Config;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options
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

        public Guid? TaskConfigID { get; set; }
        public TaskConfig? TaskConfig { get; set; }

        public TaskModuleParameterEnumValueEntity ParameterEnumValue { get; set; }

        public IList<TaskModuleParameterEntity> Parameters { get; set; }

        #endregion

        #region Data

        public string ShortName { get; set; }
        public ModuleType PackageType { get; set; }

        #endregion
    }
}
