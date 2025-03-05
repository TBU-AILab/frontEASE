namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters
{
    public interface ITaskModuleParameterBase
    {
        public string Key { get; set; }
        public string ShortName { get; set; }
        public string Type { get; set; }
    }
}
