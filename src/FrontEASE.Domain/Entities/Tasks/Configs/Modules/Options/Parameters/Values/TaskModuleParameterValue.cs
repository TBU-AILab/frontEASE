using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.Enum;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values
{
    public class TaskModuleParameterValue : ITaskModuleParameterValueBase
    {
        #region Data

        public TaskModuleParameterListOption? ListValue { get; set; }
        public TaskModuleParameterEnumOption? EnumValue { get; set; }
        public double? FloatValue { get; set; }
        public int? IntValue { get; set; }
        public string? StringValue { get; set; }
        public bool? BoolValue { get; set; }

        #endregion
    }
}
