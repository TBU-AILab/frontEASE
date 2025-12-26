using Blazored.LocalStorage;
using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using FrontEASE.Client;
using FrontEASE.Client.Infrastructure.Mappings.Companies;
using FrontEASE.Client.Infrastructure.Mappings.Management;
using FrontEASE.Client.Infrastructure.Mappings.Management.Core;
using FrontEASE.Client.Infrastructure.Mappings.Management.General;
using FrontEASE.Client.Infrastructure.Mappings.Management.Tokens;
using FrontEASE.Client.Infrastructure.Mappings.Management.Tokens.Connectors;
using FrontEASE.Client.Infrastructure.Mappings.Shared.Addresses;
using FrontEASE.Client.Infrastructure.Mappings.Shared.Files;
using FrontEASE.Client.Infrastructure.Mappings.Shared.Images;
using FrontEASE.Client.Infrastructure.Mappings.Shared.Users;
using FrontEASE.Client.Infrastructure.Mappings.Tasks;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Actions.Requests;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Metadata;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Options.Enum;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Options.List;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.RepeatedMessage;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Messages;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Shared;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Solutions;
using FrontEASE.Client.Infrastructure.Settings.AppSettings;
using FrontEASE.Client.Services.ApiServices.Companies;
using FrontEASE.Client.Services.ApiServices.Management;
using FrontEASE.Client.Services.ApiServices.Shared.Core;
using FrontEASE.Client.Services.ApiServices.Shared.Files;
using FrontEASE.Client.Services.ApiServices.Shared.Resources;
using FrontEASE.Client.Services.ApiServices.Shared.Typelists;
using FrontEASE.Client.Services.ApiServices.Shared.Users;
using FrontEASE.Client.Services.ApiServices.Tasks;
using FrontEASE.Client.Services.HelperServices.Auth;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Client.Services.HelperServices.Resources.Manage;
using FrontEASE.Client.Services.HelperServices.UI.Manage;
using FrontEASE.Client.Services.HelperServices.UI.Operations;
using FrontEASE.Client.Services.HelperServices.UI.Signals;
using FrontEASE.Client.Services.ModelManipulationServices.Companies;
using FrontEASE.Client.Services.ModelManipulationServices.Management;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts;
using FrontEASE.Client.Services.ModelManipulationServices.User;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Caching.Memory;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var settings = (AppSettings?)null;

SetupArchitecture();
SetupLogging();
SetupAuth();
SetupUIEnhancements();
SetupHelperServices();
SetupApiServices();
SetupModelManipulationServices();
SetupUtils();
SetupMappings();

await builder.Build().RunAsync();

void SetupUIEnhancements()
{
    builder.Services.AddBlazorise(options =>
    {
        options.ChangeSliderOnHold = true;
        options.EnableNumericStep = true;
        options.ShowNumericStepButtons = true;
        options.Immediate = false;
        options.DebounceInterval = 50;
        options.Debounce = true;

        options.ProductToken = settings!.LicenseSettings!.Blazorise!.LicenseToken;
    }).AddBootstrap5Providers().AddFontAwesomeIcons();
    builder.Services.AddBlazoredToast();
}

void SetupMappings()
{
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.AddProfile(new FileMappingProfile());
        cfg.AddProfile(new ImageMappingProfile());
        cfg.AddProfile(new UserMappingProfile());
        cfg.AddProfile(new CompanyMappingProfile());
        cfg.AddProfile(new AddressMappingProfile());

        cfg.AddProfile(new UserPreferencesMappingProfile());
        cfg.AddProfile(new UserPreferenceTokenOptionMappingProfile());
        cfg.AddProfile(new UserPreferenceGeneralOptionsMappingProfile());
        cfg.AddProfile(new UserPreferencesTokenOptionConnectorMappingProfile());
        cfg.AddProfile(new GlobalPreferencesMappingProfile());
        cfg.AddProfile(new CorePackageMappingProfile());
        cfg.AddProfile(new CoreModuleMappingProfile());

        cfg.AddProfile(new TaskMappingProfile());
        cfg.AddProfile(new TaskStatusMappingProfile());
        cfg.AddProfile(new TaskInfoMappingProfile());
        cfg.AddProfile(new TaskMessageMappingProfile());
        cfg.AddProfile(new TaskSolutionMappingProfile());
        cfg.AddProfile(new TaskConfigMappingProfile());
        cfg.AddProfile(new TaskConfigRepeatedMessageProfile());
        cfg.AddProfile(new TaskConfigRepeatedMessageItemProfile());
        cfg.AddProfile(new TaskKeyValueItemMappingProfile());
        cfg.AddProfile(new TaskModuleMappingProfile());
        cfg.AddProfile(new TaskModuleParameterMappingProfile());
        cfg.AddProfile(new TaskModuleParameterValueMappingProfile());
        cfg.AddProfile(new TaskModuleParameterEnumOptionMappingProfile());
        cfg.AddProfile(new TaskModuleParameterListOptionMappingProfile());
        cfg.AddProfile(new TaskModuleParameterListOptionParamsMappingProfile());
        cfg.AddProfile(new TaskModuleParameterNoValidationMetadataMappingProfile());

        cfg.AddProfile(new TaskDuplicateActionRequestMappingProfile());
        cfg.AddProfile(new TaskFilterActionRequestMappingProfile());
    });
}

void SetupAuth()
{
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddAuthorizationCore();
    builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
}

void SetupArchitecture()
{
    settings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
    builder.Services.AddSingleton(settings!);
    builder.Services.AddBlazoredLocalStorage();
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(settings!.ApiSettings!.BaseUrl!) });
}

void SetupLogging()
{
    builder.Logging.AddFilter("LuckyPennySoftware.AutoMapper.License", LogLevel.None);
}

void SetupUtils()
{
    builder!.Services.AddSingleton<IResourceHandler, ResourceHandler>();
    builder!.Services.AddScoped<IResourcesManager, ResourcesManager>();
    builder!.Services.AddSingleton<IUIManager, UIManager>();
    builder!.Services.AddScoped<IUIService, UIService>();
}

void SetupHelperServices()
{
    builder!.Services.AddTransient<IErrorHandlingService, ErrorHandlingService>();
    builder!.Services.AddScoped<IDataLoader, DataLoader>();
    builder!.Services.AddSingleton<IMemoryCache, MemoryCache>();
}

void SetupApiServices()
{
    builder!.Services.AddTransient<IResourceApiService, ResourceApiService>();
    builder!.Services.AddTransient<ICompanyApiService, CompanyApiService>();
    builder!.Services.AddTransient<IUserApiService, UserApiService>();
    builder!.Services.AddTransient<ITaskApiService, TaskApiService>();
    builder!.Services.AddTransient<IFileApiService, FileApiService>();
    builder!.Services.AddTransient<IManagementApiService, ManagementApiService>();
    builder!.Services.AddTransient<ITypelistApiService, TypelistApiService>();
    builder!.Services.AddTransient<ICoreApiService, CoreApiService>();
}
void SetupModelManipulationServices()
{
    builder!.Services.AddTransient<ICompanyManipulationService, CompanyManipulationService>();
    builder!.Services.AddTransient<IUserManipulationService, UserManipulationService>();
    builder!.Services.AddTransient<ITaskManipulationService, TaskManipulationService>();
    builder!.Services.AddTransient<IManagementManipulationService, ManagementManipulationService>();
    builder!.Services.AddTransient<ITaskChartManipulationService, TaskChartManipulationService>();
}
