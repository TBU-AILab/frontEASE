using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters
{
    public class TaskModuleParameterDto
    {
        public TaskModuleParameterDto()
        {
            ShortName = string.Empty;
            Type = string.Empty;
            Key = string.Empty;
        }

        public string Key { get; set; }
        public string ShortName { get; set; }
        public string Type { get; set; }
        public TaskModuleParameterValueDto? Value { get; set; }
    }
}
