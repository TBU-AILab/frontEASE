using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values
{
    public class TaskModuleParameterValue : ITaskModuleParameterValueBase
    {
        #region Data

        public TaskModuleParameterEnumOption? EnumValue { get; set; }
        public float? FloatValue { get; set; }
        public int? IntValue { get; set; }
        public string? StringValue { get; set; }
        public bool? BoolValue { get; set; }

        #endregion
    }
}
