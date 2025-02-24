using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;

namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters
{
    public class TaskModuleParameter : ITaskModuleParameterBase
    {
        public TaskModuleParameter()
        {
            Key = string.Empty;
            ShortName = string.Empty;
            Type = string.Empty;

            EnumOptions = [];
        }

        #region Navigation

        public TaskModuleParameterValue? Value { get; set; }
        public TaskModuleParameterValue? Default { get; set; }
        public IList<TaskModuleParameterEnumOption> EnumOptions { get; set; }

        #endregion

        #region Data

        public string Key { get; set; }
        public string ShortName { get; set; }
        public string? LongName { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public float? MinValue { get; set; }
        public float? MaxValue { get; set; }
        public IList<string>? EnumDescriptions { get; set; }
        public IList<string>? EnumLongNames { get; set; }
        public bool? Readonly { get; set; }

        #endregion
    }
}
