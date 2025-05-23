﻿using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters;
using FrontEASE.Shared.Data.Enums.Tasks.Config;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules
{
    public class TaskModuleCoreDto : TaskModuleBaseCoreDto, ICoreDto
    {
        public TaskModuleCoreDto()
        {
            LongName = string.Empty;
            Description = string.Empty;

            Parameters = new Dictionary<string, TaskModuleParameterCoreDto>();
        }

        [JsonPropertyName("long_name")]
        public string LongName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("package_type")]
        public ModuleType PackageType { get; set; }

        [JsonPropertyName("parameters")]
        public IDictionary<string, TaskModuleParameterCoreDto> Parameters { get; set; }
    }
}
