namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List
{
    public class TaskModuleParameterListOptionParams
    {
        public TaskModuleParameterListOptionParams()
        {
            ParameterItems = [];
        }

        public IList<TaskModuleParameter> ParameterItems { get; set; }
    }
}
