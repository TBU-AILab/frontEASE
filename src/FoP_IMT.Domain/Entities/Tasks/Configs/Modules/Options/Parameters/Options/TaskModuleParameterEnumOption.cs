namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options
{
    public class TaskModuleParameterEnumOption : ITaskModuleParameterEnumOption
    {
        #region Data

        public string? StringValue { get; set; }
        public TaskModule? ModuleValue { get; set; }

        #endregion
    }
}
