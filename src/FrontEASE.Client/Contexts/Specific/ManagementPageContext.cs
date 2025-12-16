using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FrontEASE.Client.Contexts.Specific
{
    public class ManagementPageContext : ApplicationContext
    {
        public ManagementPageContext(ApplicationContext appContext)
        {
            LoggedInUser = appContext.LoggedInUser;
            UserPreferences = appContext.UserPreferences;

            ConnectorTypes = [];
            ModuleTypes = [];
        }

        public IList<TaskModuleNoValidationDto> ConnectorTypes { get; set; }
        public IList<TaskModuleNoValidationDto> ModuleTypes { get; set; }
    }
}
