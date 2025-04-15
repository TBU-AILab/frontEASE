namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options
{
    public class TaskModuleParameterListOptionCoreDto : ICoreDto
    {
        public TaskModuleParameterListOptionCoreDto()
        {
            ParameterValues = new Dictionary<string, TaskModuleParameterCoreDto>();
        }

        public IDictionary<string, TaskModuleParameterCoreDto> ParameterValues { get; set; }
    }
}
