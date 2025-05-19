namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List
{
    public class TaskModuleParameterListOption
    {
        public TaskModuleParameterListOption()
        {
            ParameterValues = [];
        }

        public IList<TaskModuleParameterListOptionParams> ParameterValues { get; set; }
    }
}
