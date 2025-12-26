using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Data.Enums.Tasks.Config;

namespace FrontEASE.Client.Contexts.Specific.Tasks
{
    public class TaskModuleContext
    {
        public TaskModuleContext(
            TaskModuleDto module,
            IList<TaskModuleNoValidationDto> moduleOptions,
            string? moduleName = null)
        {
            ModuleOptions = moduleOptions;
            Module = module;
            ModuleName = moduleName;

            ModuleData = ModuleOptions.FirstOrDefault(x => x.ShortName == Module.ShortName);
            ModuleType = ModuleOptions.FirstOrDefault()?.PackageType;
        }

        public IList<TaskModuleNoValidationDto> ModuleOptions { get; set; }
        public TaskModuleDto Module { get; set; }
        public TaskModuleNoValidationDto? ModuleData { get; set; }
        public string? ModuleName { get; set; }
        public ModuleType? ModuleType { get; set; }
    }
}
