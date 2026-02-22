using FrontEASE.Shared.Data.DTOs.Management.Tags;
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
            AllTags = [];
        }

        public IList<TaskModuleNoValidationDto> ConnectorTypes { get; set; }
        public IList<TaskModuleNoValidationDto> ModuleTypes { get; set; }
        public IList<UserPreferenceTagOptionDto> AllTags { get; set; }
    }
}
