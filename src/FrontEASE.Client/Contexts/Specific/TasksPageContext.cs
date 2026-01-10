using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using System.Collections.ObjectModel;

namespace FrontEASE.Client.Contexts.Specific
{
    public class TasksPageContext : ApplicationContext
    {
        public TasksPageContext(ApplicationContext appContext)
        {
            LoggedInUser = appContext.LoggedInUser;
            UserPreferences = appContext.UserPreferences;

            ViewMetadata = new();
            Tasks = [];
            TaskModuleOptions = [];
            AvailableUsers = [];
            AvailableCompanies = [];
        }

        public TaskListViewMetadataDto ViewMetadata { get; set; }
        public ObservableCollection<TaskInfoDto> Tasks { get; set; }

        public IList<TaskModuleNoValidationDto> TaskModuleOptions { get; set; }
        public IList<ApplicationUserDto> AvailableUsers { get; set; }
        public IList<CompanyDto> AvailableCompanies { get; set; }
    }
}
