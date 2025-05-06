namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.Enum
{
    public class TaskModuleParameterEnumOption : ITaskModuleParameterEnumOption
    {
        #region Data

        public string? StringValue { get; set; }
        public TaskModule? ModuleValue { get; set; }

        #endregion
    }
}
