using FoP_IMT.Shared.Data.Enums.Tasks.Config;

namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options
{
    public interface ITaskModuleBase
    {
        public string ShortName { get; set; }
        public ModuleType PackageType { get; set; }
    }
}
