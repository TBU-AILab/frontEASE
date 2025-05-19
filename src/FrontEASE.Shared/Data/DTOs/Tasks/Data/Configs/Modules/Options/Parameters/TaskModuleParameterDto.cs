using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters
{
    /// <summary>
    /// Value of module parameter
    /// </summary>
    public class TaskModuleParameterDto
    {
        public TaskModuleParameterDto()
        {
            ShortName = string.Empty;
            Type = string.Empty;
            Key = string.Empty;
        }

        /// <summary>
        /// Key - identifier
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Parameter short name (code)
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Parameter type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Parameter value - nested object
        /// </summary>
        public TaskModuleParameterValueDto? Value { get; set; }
    }
}
