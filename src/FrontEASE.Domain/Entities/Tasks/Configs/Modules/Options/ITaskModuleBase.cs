using FrontEASE.Shared.Data.Enums.Tasks.Config;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options
{
    public interface ITaskModuleBase
    {
        public string ShortName { get; set; }
        public ModuleType PackageType { get; set; }
    }
}
