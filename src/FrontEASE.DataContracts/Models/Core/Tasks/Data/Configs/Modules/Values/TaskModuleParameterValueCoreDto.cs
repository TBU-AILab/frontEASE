﻿using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;

namespace FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values
{
    public class TaskModuleParameterValueCoreDto : ICoreDto
    {
        #region Data

        public float? FloatValue { get; set; }
        public int? IntValue { get; set; }
        public string? StringValue { get; set; }
        public bool? BoolValue { get; set; }
        public TaskModuleParameterListOptionCoreDto? ListValue { get; set; }
        public TaskModuleParameterEnumOptionCoreDto? EnumValue { get; set; }

        #endregion
    }
}
