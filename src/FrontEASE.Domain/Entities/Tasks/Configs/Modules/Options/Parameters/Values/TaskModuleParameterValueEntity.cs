using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.Enum;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values
{
    public class TaskModuleParameterValueEntity : EntityBase, ITaskModuleParameterValueBase
    {
        public TaskModuleParameterValueEntity()
        {
            Parameter = null!;
        }

        #region Navigation

        public TaskModuleParameterEntity Parameter { get; set; }

        public Guid? EnumValueID { get; set; }
        public TaskModuleParameterEnumValueEntity? EnumValue { get; set; }

        public Guid? ListValueID { get; set; }
        public TaskModuleParameterListValueEntity? ListValue { get; set; }

        #endregion

        #region Data

        public double? FloatValue { get; set; }
        public int? IntValue { get; set; }
        public string? StringValue { get; set; }
        public bool? BoolValue { get; set; }

        #endregion
    }
}
