namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options
{
    public class TaskModuleParameterListOption
    {
        public TaskModuleParameterListOption()
        {
            ParameterValues = new Dictionary<string, TaskModuleParameter>();
        }

        public IDictionary<string, TaskModuleParameter> ParameterValues { get; set; }
    }
}
