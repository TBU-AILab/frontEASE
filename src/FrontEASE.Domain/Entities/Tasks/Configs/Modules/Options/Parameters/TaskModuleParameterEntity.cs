using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters
{
    public class TaskModuleParameterEntity : EntityBase, ITaskModuleParameterBase
    {
        public TaskModuleParameterEntity()
        {
            Module = null!;

            Key = string.Empty;
            ShortName = string.Empty;
            Type = string.Empty;
        }

        #region Navigation

        public Guid ModuleID { get; set; }
        public TaskModuleEntity Module { get; set; }

        public Guid? ListValueID { get; set; }
        public TaskModuleParameterListValueItemEntity? ListValue { get; set; }

        public Guid? ValueID { get; set; }
        public TaskModuleParameterValueEntity? Value { get; set; }

        #endregion

        #region Data

        public string Key { get; set; }
        public string ShortName { get; set; }
        public string Type { get; set; }

        #endregion
    }
}
