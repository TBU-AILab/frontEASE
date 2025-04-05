using AutoMapper;
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
using FrontEASE.Client.Infrastructure.Mappings.Shared.Images;
using FrontEASE.Client.Infrastructure.Mappings.Tasks;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Actions.Requests;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Metadata;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Options;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.RepeatedMessage;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Messages;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Shared;
using FrontEASE.Client.Infrastructure.Mappings.Tasks.Solutions;
using FrontEASE.Client.Infrastructure.Settings.AppSettings;
using FrontEASE.Client.Services.ApiServices.Companies;
using FrontEASE.Client.Services.ApiServices.Management;
using FrontEASE.Client.Services.ApiServices.Shared.Files;
using FrontEASE.Client.Services.ApiServices.Shared.Resources;
using FrontEASE.Client.Services.ApiServices.Shared.Typelists;
using FrontEASE.Client.Services.ApiServices.Shared.Users;
using FrontEASE.Client.Services.ApiServices.Tasks;
using FrontEASE.Client.Services.HelperServices;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Client.Services.HelperServices.Resources.Manage;
using FrontEASE.Client.Services.HelperServices.UI.Manage;
using FrontEASE.Client.Services.HelperServices.UI.Operations;
using FrontEASE.Client.Services.HelperServices.UI.Signals;
using FrontEASE.Client.Services.ModelManipulationServices.Companies;
using FrontEASE.Client.Services.ModelManipulationServices.Management;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks;
using FrontEASE.Client.Services.ModelManipulationServices.User;
using FrontEASE.Client.Shared.Dictionaries.Shortcuts;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Caching.Memory;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
AppSettings? settings = null;

SetupArchitecture();
SetupAuth();
SetupUIEnhancements();
SetupHelperServices();
SetupApiServices();
SetupModelManipulationServices();
SetupDictionaries();
SetupUtils();
SetupMappings();
SetupUIEnhancements();

await builder.Build().RunAsync();

void SetupUIEnhancements()
{
    builder.Services.AddBlazorise(options =>
    {
        options.ChangeSliderOnHold = true;
        options.EnableNumericStep = true;
        options.ShowNumericStepButtons = true;
        options.Immediate = true;
        options.DebounceInterval = 500;
        options.Debounce = true;
    }).AddBootstrap5Providers().AddFontAwesomeIcons();
    builder.Services.AddBlazoredToast();
}

void SetupMappings()
{
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new ImageMappingProfile());
        mc.AddProfile(new UserMappingProfile());
        mc.AddProfile(new CompanyMappingProfile());
        mc.AddProfile(new AddressMappingProfile());

        mc.AddProfile(new UserPreferencesMappingProfile());
        mc.AddProfile(new UserPreferenceTokenOptionMappingProfile());
        mc.AddProfile(new UserPreferenceGeneralOptionsMappingProfile());
        mc.AddProfile(new UserPreferencesTokenOptionConnectorMappingProfile());
        mc.AddProfile(new GlobalPreferencesMappingProfile());
        mc.AddProfile(new CorePackageMappingProfile());

        mc.AddProfile(new TaskMappingProfile());
        mc.AddProfile(new TaskStatusMappingProfile());
        mc.AddProfile(new TaskInfoMappingProfile());
        mc.AddProfile(new TaskMessageMappingProfile());
        mc.AddProfile(new TaskSolutionMappingProfile());
        mc.AddProfile(new TaskConfigMappingProfile());
        mc.AddProfile(new TaskConfigRepeatedMessageProfile());
        mc.AddProfile(new TaskConfigRepeatedMessageItemProfile());
        mc.AddProfile(new TaskKeyValueItemMappingProfile());
        mc.AddProfile(new TaskModuleMappingProfile());
        mc.AddProfile(new TaskModuleParameterMappingProfile());
        mc.AddProfile(new TaskModuleParameterValueMappingProfile());
        mc.AddProfile(new TaskModuleParameterEnumOptionMappingProfile());
        mc.AddProfile(new TaskModuleParameterNoValidationMetadataMappingProfile());

        mc.AddProfile(new TaskDuplicateActionRequestMappingProfile());
        mc.AddProfile(new TaskFilterActionRequestMappingProfile());
    });

    IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
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
}
void SetupModelManipulationServices()
{
    builder!.Services.AddTransient<ICompanyManipulationService, CompanyManipulationService>();
    builder!.Services.AddTransient<IUserManipulationService, UserManipulationService>();
    builder!.Services.AddTransient<ITaskManipulationService, TaskManipulationService>();
    builder!.Services.AddTransient<IManagementManipulationService, ManagementManipulationService>();
}

void SetupDictionaries()
{
    builder!.Services.AddTransient<IShortcutKeyDictionary, ShortcutKeyDictionary>();
}
