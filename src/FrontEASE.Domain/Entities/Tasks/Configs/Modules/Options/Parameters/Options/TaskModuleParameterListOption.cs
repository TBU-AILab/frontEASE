namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options
{
    public class TaskModuleParameterListOption
    {
        public TaskModuleParameterListOption()
        {
            ParameterValues = new List<IList<TaskModuleParameter>>();
        }

        public IList<IList<TaskModuleParameter>> ParameterValues { get; set; }
    }
}
