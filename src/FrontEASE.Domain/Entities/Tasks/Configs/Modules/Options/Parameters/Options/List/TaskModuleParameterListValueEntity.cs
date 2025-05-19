using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List
{
    public class TaskModuleParameterListValueEntity : EntityBase
    {
        public TaskModuleParameterListValueEntity()
        {
            ParameterValue = null!;
            ParameterValues = [];
        }

        #region Navigation

        public TaskModuleParameterValueEntity ParameterValue { get; set; }
        public IList<TaskModuleParameterListValueItemEntity> ParameterValues { get; set; }

        #endregion
    }
}
