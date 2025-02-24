using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;

namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values
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

        #endregion

        #region Data

        public float? FloatValue { get; set; }
        public int? IntValue { get; set; }
        public string? StringValue { get; set; }
        public bool? BoolValue { get; set; }

        #endregion
    }
}
