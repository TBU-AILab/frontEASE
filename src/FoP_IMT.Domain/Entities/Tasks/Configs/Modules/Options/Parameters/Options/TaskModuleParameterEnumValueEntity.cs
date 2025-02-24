using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;

namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options
{
    public class TaskModuleParameterEnumValueEntity : EntityBase, ITaskModuleParameterEnumOption
    {
        public TaskModuleParameterEnumValueEntity()
        {
            ParameterValue = null!;
        }

        #region Navigation

        public TaskModuleParameterValueEntity ParameterValue { get; set; }

        public Guid? ModuleValueID { get; set; }
        public TaskModuleEntity? ModuleValue { get; set; }

        #endregion

        #region Data

        public string? StringValue { get; set; }

        #endregion
    }
}
