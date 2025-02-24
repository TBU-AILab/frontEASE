using AutoMapper;
using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using FoP_IMT.Client.Infrastructure.Mappings.Companies;
using FoP_IMT.Client.Infrastructure.Mappings.Management;
using FoP_IMT.Client.Infrastructure.Mappings.Management.General;
using FoP_IMT.Client.Infrastructure.Mappings.Management.Tokens;
using FoP_IMT.Client.Infrastructure.Mappings.Shared.Addresses;
using FoP_IMT.Client.Infrastructure.Mappings.Shared.Images;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Metadata;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Options;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.RepeatedMessage;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks.Messages;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks.Shared;
using FoP_IMT.Client.Infrastructure.Mappings.Tasks.Solutions;
using FoP_IMT.Client.Infrastructure.Middleware;
using FoP_IMT.Client.Infrastructure.Settings.AppSettings;
using FoP_IMT.Client.Services.ApiServices.Companies;
using FoP_IMT.Client.Services.ApiServices.Management;
using FoP_IMT.Client.Services.ApiServices.Shared.Files;
using FoP_IMT.Client.Services.ApiServices.Shared.Resources;
using FoP_IMT.Client.Services.ApiServices.Shared.Typelists;
using FoP_IMT.Client.Services.ApiServices.Shared.Users;
using FoP_IMT.Client.Services.ApiServices.Tasks;
using FoP_IMT.Client.Services.HelperServices.ErrorHandling;
using FoP_IMT.Client.Services.HelperServices.Resources.Manage;
using FoP_IMT.Client.Services.HelperServices.UI.Manage;
using FoP_IMT.Client.Services.HelperServices.UI.Operations;
using FoP_IMT.Client.Services.HelperServices.UI.Signals;
using FoP_IMT.Client.Services.ModelManipulationServices.Companies;
using FoP_IMT.Client.Services.ModelManipulationServices.Management;
using FoP_IMT.Client.Services.ModelManipulationServices.Tasks;
using FoP_IMT.Client.Services.ModelManipulationServices.User;
using FoP_IMT.Shared.Services.Resources;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Caching.Memory;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
AppSettings? settings = null;

SetupClientArchitecture();
SetupAuth();
SetupClientConnectionHandling();
SetupClientUIEnhancements();

/* Client */
SetupClientHelperServices();
SetupClientApiServices();
SetupClientModelManipulationServices();
SetupClientUtils();
SetupClientMappings();

await builder.Build().RunAsync();

void SetupClientArchitecture()
{
    settings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
    builder.Services.AddSingleton(settings!);
}

void SetupClientConnectionHandling()
{
    const string clientName = "FoP_IMT.ServerAPI";

    builder.Services.AddHttpClient(clientName, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
    builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(clientName));
}

void SetupClientUIEnhancements()
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

void SetupAuth()
{
    builder.Services.AddAuthorizationCore();
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
}

void SetupClientMappings()
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
    });

    IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
}

void SetupClientUtils()
{
    builder!.Services.AddSingleton<IResourceHandler, ResourceHandler>();
    builder!.Services.AddScoped<IResourcesManager, ResourcesManager>();
    builder!.Services.AddSingleton<IUIManager, UIManager>();
    builder!.Services.AddScoped<IUIService, UIService>();
}

void SetupClientHelperServices()
{
    builder!.Services.AddTransient<IErrorHandlingService, ErrorHandlingService>();
    builder!.Services.AddScoped<IDataLoader, DataLoader>();
    builder!.Services.AddSingleton<IMemoryCache, MemoryCache>();
}

void SetupClientApiServices()
{
    builder!.Services.AddTransient<IResourceApiService, ResourceApiService>();
    builder!.Services.AddTransient<ICompanyApiService, CompanyApiService>();
    builder!.Services.AddTransient<IUserApiService, UserApiService>();
    builder!.Services.AddTransient<ITaskApiService, TaskApiService>();
    builder!.Services.AddTransient<IFileApiService, FileApiService>();
    builder!.Services.AddTransient<IManagementApiService, ManagementApiService>();
    builder!.Services.AddTransient<ITypelistApiService, TypelistApiService>();
}

void SetupClientModelManipulationServices()
{
    builder!.Services.AddTransient<ICompanyManipulationService, CompanyManipulationService>();
    builder!.Services.AddTransient<IUserManipulationService, UserManipulationService>();
    builder!.Services.AddTransient<ITaskManipulationService, TaskManipulationService>();
    builder!.Services.AddTransient<IManagementManipulationService, ManagementManipulationService>();
}