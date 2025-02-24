using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;

namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters
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
