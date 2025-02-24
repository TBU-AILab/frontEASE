﻿using System.Text.Json.Serialization;

namespace FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules
{
    public class TaskModuleInputCoreDto : TaskModuleBaseCoreDto, ITaskCoreDto
    {
        public TaskModuleInputCoreDto()
        {
            Parameters = new Dictionary<string, object>();
        }

        [JsonPropertyName("parameters")]
        public IDictionary<string, object> Parameters { get; set; }
    }
}
