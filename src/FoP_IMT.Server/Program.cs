using AutoMapper;
using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using FoP_IMT.Application.AppServices.Companies;
using FoP_IMT.Application.AppServices.Files;
using FoP_IMT.Application.AppServices.Management;
using FoP_IMT.Application.AppServices.Shared.Resources;
using FoP_IMT.Application.AppServices.Shared.Typelists;
using FoP_IMT.Application.AppServices.Tasks;
using FoP_IMT.Application.AppServices.Users;
using FoP_IMT.Application.Infrastructure.Jobs;
using FoP_IMT.Application.Infrastructure.Jobs.Tasks;
using FoP_IMT.Application.Infrastructure.Mappings.Companies;
using FoP_IMT.Application.Infrastructure.Mappings.Management;
using FoP_IMT.Application.Infrastructure.Mappings.Management.General;
using FoP_IMT.Application.Infrastructure.Mappings.Management.Tokens;
using FoP_IMT.Application.Infrastructure.Mappings.Shared.Addresses;
using FoP_IMT.Application.Infrastructure.Mappings.Shared.Images;
using FoP_IMT.Application.Infrastructure.Mappings.Shared.Resources;
using FoP_IMT.Application.Infrastructure.Mappings.Tasks;
using FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs;
using FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs.Modules;
using FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters;
using FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values;
using FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs.Modules.RepeatedMessage;
using FoP_IMT.Application.Infrastructure.Mappings.Tasks.Messages;
using FoP_IMT.Application.Infrastructure.Mappings.Tasks.Shared;
using FoP_IMT.Application.Infrastructure.Mappings.Tasks.Solutions;
using FoP_IMT.DataContracts.Models.Core;
using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Domain.Infrastructure.Settings.Connection;
using FoP_IMT.Domain.Repositories.Companies;
using FoP_IMT.Domain.Repositories.Management;
using FoP_IMT.Domain.Repositories.Shared.Resources;
using FoP_IMT.Domain.Repositories.Tasks;
using FoP_IMT.Domain.Repositories.Users;
using FoP_IMT.Domain.Services.Companies;
using FoP_IMT.Domain.Services.Management;
using FoP_IMT.Domain.Services.Shared.Files;
using FoP_IMT.Domain.Services.Shared.Images;
using FoP_IMT.Domain.Services.Shared.Logging;
using FoP_IMT.Domain.Services.Shared.Logging.Sentry;
using FoP_IMT.Domain.Services.Shared.Resources;
using FoP_IMT.Domain.Services.Shared.Typelists;
using FoP_IMT.Domain.Services.Tasks;
using FoP_IMT.Domain.Services.Tasks.Core;
using FoP_IMT.Domain.Services.Users;
using FoP_IMT.Infrastructure.Data;
using FoP_IMT.Infrastructure.HealthChecks;
using FoP_IMT.Infrastructure.Repositories.Companies;
using FoP_IMT.Infrastructure.Repositories.Management;
using FoP_IMT.Infrastructure.Repositories.Shared.Resources;
using FoP_IMT.Infrastructure.Repositories.Tasks;
using FoP_IMT.Infrastructure.Repositories.Users;
using FoP_IMT.Server.Components;
using FoP_IMT.Server.Infrastructure.Auth;
using FoP_IMT.Server.Infrastructure.Auth.Extensions;
using FoP_IMT.Server.Infrastructure.Hangfire.Attributes;
using FoP_IMT.Server.Infrastructure.Hangfire.Filters;
using FoP_IMT.Server.Infrastructure.Middleware;
using FoP_IMT.Server.Infrastructure.Swagger.Filters.Documentation;
using FoP_IMT.Shared.Data.DTOs.Shared.Exceptions;
using FoP_IMT.Shared.Infrastructure.Auth;
using FoP_IMT.Shared.Services.Resources;
using Hangfire;
using Hangfire.Console;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Security.Claims;

/* INITIALIZE */
var executingAssembly = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);
var settings = (AppSettings?)null;
var connection = (ConnectionStrings?)null;

var isGeneratorRun = executingAssembly?.Location?.Contains("DataGenerator") ?? false;

/* Basic */
SetupArchitecture();

/* Global */
SetupDatabase();
SetupJobs();
SetupMvc();
SetupAuth();
SetupSwaggerGen();
SetupMonitoring();
SetupUIEnhancements();

/* Server */
SetupServerRepositories();
SetupServerServices();
SetupServerAppServices();
SetupServerHelperServices();
SetupServerHttpClients();
SetupServerMappings();
SetupServerJobServices();

/* BUILD APP */
var app = builder.Build();

/* Runtime */
ConfigureArchitecture();
ConfigureSwagger();
ConfigureJobs();
ConfigureMonitoring();
ConfigureFramework();
ConfigureMvc();
ConfigureView();

/* RUN */
await app.RunAsync();

void ConfigureView()
{
    app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(FoP_IMT.Client._Imports).Assembly);
}

void ConfigureMonitoring()
{
    if (settings?.SentrySettings?.IsEnabled == true)
    {
        app.UseCors("AllowSentryTrace");
        app.UseSentryTracing();
    }
}

void ConfigureArchitecture()
{
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseHsts();
    }

    app.UseHealthChecks("/api/health");
    app.UseStatusCodePagesWithRedirects("/Error/{0}");
}

void ConfigureSwagger()
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/Swagger/v1/swagger.json", "API - FoP IMT");
    });
}

void ConfigureAuth()
{
    var fordwardedHeaderOptions = new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    };
    fordwardedHeaderOptions.KnownNetworks.Clear();
    fordwardedHeaderOptions.KnownProxies.Clear();
    app.UseForwardedHeaders(fordwardedHeaderOptions);

    app.UseAuthorization();
    app.MapAdditionalIdentityEndpoints();
}

void ConfigureJobs()
{
    if (settings!.HangfireSettings?.IsEnabled == true && !isGeneratorRun)
    {
        var options = new DashboardOptions
        {
            Authorization = new[] { new HangfireIdentityAuthorizationFilter(app.Services.GetRequiredService<IHttpContextAccessor>()) },
            DashboardTitle = "FoP_IMT - Jobs",
        };

        app.UseHangfireDashboard("/Hangfire", options);
        GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());
        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() { Attempts = 3 });
        JobFactory.ScheduleJobs(settings!, app.Services);
    }
}

void ConfigureMvc()
{
    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");
}

void ConfigureFramework()
{
    app.UseHttpsRedirection();
    app.UseBlazorFrameworkFiles();
    app.UseRouting();

    ConfigureAuth();

    app.UseAntiforgery();
    app.UseStaticFiles();
    app.UseRequestLocalization("en-US");
}

void SetupAuth()
{
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddScoped<IdentityUserAccessor>();
    builder.Services.AddScoped<IdentityRedirectManager>();
    builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

    builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddUserManager<UserManager<ApplicationUser>>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();
}


void SetupArchitecture()
{
    settings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
    connection = builder.Configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

    builder.Services.AddSingleton(settings!);
    builder.Services.AddSingleton(connection!);

    builder.Services.AddHealthChecks().AddCheck<ConnectivityHealthCheck>("Connectivity");
}

void SetupMvc()
{
    builder.Services.AddMvc();
    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents()
        .AddInteractiveWebAssemblyComponents();
    builder.Services.AddLocalization();
}


void SetupDatabase()
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection!.DefaultConnection));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

void SetupJobs()
{
    if (settings!.HangfireSettings!.IsEnabled && !isGeneratorRun)
    {
        builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(connection!.DefaultConnection!);
            }, new PostgreSqlStorageOptions
            {
                DistributedLockTimeout = TimeSpan.FromMinutes(5),
                InvisibilityTimeout = TimeSpan.FromMinutes(5),
                JobExpirationCheckInterval = TimeSpan.FromMinutes(5),
                UseSlidingInvisibilityTimeout = true,
            })
            .UseConsole()
        );

        builder.Services.AddHangfireServer(e =>
        {
            e.WorkerCount = 2;
        });
    }
}

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

void SetupMonitoring()
{
    if (settings?.SentrySettings?.IsEnabled == true)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSentryTrace",
                b => b.AllowAnyOrigin()
                    .WithHeaders("sentry-trace", "baggage"));
        });

        var sentrySettings = settings?.SentrySettings!;
        builder.WebHost.UseSentry(o =>
        {
            o.Dsn = sentrySettings.DSN;
            o.Release = sentrySettings.Release;
            o.Environment = sentrySettings.Environment;
            o.TracesSampler = context =>
            {
                if (context.TransactionContext.IsParentSampled is not null)
                {
                    return context.TransactionContext.IsParentSampled.Value
                        ? 1.0
                        : 0.0;
                }

                double? sampleRate = context.CustomSamplingContext.GetValueOrDefault("__HttpPath") switch
                {
                    "/swagger/index.html" => 0,
                    "/api/health" => 0,
                    "/hangfire/stats" => 0,
                    _ => 1
                };
                return sampleRate;
            };

            o.CaptureFailedRequests = true;
            o.FailedRequestStatusCodes.Add((400, 499));
            o.SetBeforeSend(sentryEvent =>
            {
                var httpContext = builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>()?.HttpContext;
                if (httpContext is null || httpContext.User?.Identity?.IsAuthenticated != true)
                { return sentryEvent; }

                if (httpContext.User?.Identity is not null)
                {
                    sentryEvent.User = new SentryUser
                    {
                        Id = httpContext.User?.Identity.Name,
                        Username = httpContext.User?.FindFirst(ClaimTypes.Email)!.Value
                    };
                }

                return sentryEvent;
            });
        });
    }
}

void SetupSwaggerGen()
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "F*ck Off People - It's Machine Time!", Version = "1.0" });
        options.DescribeAllParametersInCamelCase();
        options.SupportNonNullableReferenceTypes();

        options.OperationFilter<AuthResponsesFilter>();
        options.OperationFilter<FromQueryDictionaryFilter>();
        options.SchemaFilter<EnumSchemaFilter>();

        AddIntegrationAPIDtos(options);

        var serverAssemblyDoc = $"{Assembly.GetAssembly(typeof(Program))!.GetName().Name}.xml";
        var sharedAssemblyDoc = $"{Assembly.GetAssembly(typeof(StatusResultDto))!.GetName().Name}.xml";
        var dataContractsAssemblyDoc = $"{Assembly.GetAssembly(typeof(ITaskCoreDto))!.GetName().Name}.xml";
        var docFiles = new List<string>() { serverAssemblyDoc, sharedAssemblyDoc, dataContractsAssemblyDoc };

        foreach (var file in docFiles)
        {
            var docFilePath = Path.Combine(AppContext.BaseDirectory, file);
            if (File.Exists(docFilePath))
            {
                options.IncludeXmlComments(docFilePath, includeControllerXmlComments: true);
            }
        }

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

void AddIntegrationAPIDtos(SwaggerGenOptions options)
{
    var taskCoreDtoTypes = typeof(ITaskCoreDto).Assembly.GetTypes()
            .Where(t => typeof(ITaskCoreDto).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

    foreach (var taskCoreDtoType in taskCoreDtoTypes)
    {
        if (taskCoreDtoType.ContainsGenericParameters)
        { continue; }

        var filterDescriptor = new FilterDescriptor
        {
            Type = typeof(AddSchemaDocumentFilter<>).MakeGenericType(taskCoreDtoType),
            Arguments = []
        };
        options.DocumentFilterDescriptors.Add(filterDescriptor);
    }
}

void SetupServerMappings()
{
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new ResourceMappingProfile());
        mc.AddProfile(new ImageMappingProfile());
        mc.AddProfile(new AddressMappingProfile());

        mc.AddProfile(new CompanyMappingProfile());
        mc.AddProfile(new UserMappingProfile());

        mc.AddProfile(new UserPreferencesMappingProfile());
        mc.AddProfile(new UserPreferencesTokenOptionMappingProfile());
        mc.AddProfile(new UserPreferencesGeneralOptionsMappingProfile());

        mc.AddProfile(new TaskInfoMappingProfile());
        mc.AddProfile(new TaskStatusMappingProfile());
        mc.AddProfile(new TaskMappingProfile());
        mc.AddProfile(new TaskSolutionMappingProfile());
        mc.AddProfile(new TaskMessageMappingProfile());
        mc.AddProfile(new TaskConfigMappingProfile());
        mc.AddProfile(new TaskConfigRepeatedMessageMappingProfile());
        mc.AddProfile(new TaskConfigRepeatedMessageItemMappingProfile());
        mc.AddProfile(new TaskKeyValueItemMappingProfile());

        mc.AddProfile(new TaskModuleMappingProfile());
        mc.AddProfile(new TaskModuleParameterMappingProfile());
        mc.AddProfile(new TaskModuleParameterValueMappingProfile());
        mc.AddProfile(new TaskModuleParameterEnumOptionMappingProfile());
    });

    IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
}

void SetupServerRepositories()
{
    builder!.Services.AddTransient<IResourceRepository, ResourceRepository>();
    builder!.Services.AddTransient<ICompanyRepository, CompanyRepository>();
    builder!.Services.AddTransient<IUserRepository, UserRepository>();
    builder!.Services.AddTransient<ITaskRepository, TaskRepository>();
    builder!.Services.AddTransient<IManagementRepository, ManagementRepository>();
}

void SetupServerServices()
{
    builder!.Services.AddTransient<IResourceService, ResourceService>();
    builder!.Services.AddTransient<ICompanyService, CompanyService>();
    builder!.Services.AddTransient<IUserService, UserService>();
    builder!.Services.AddTransient<ITaskService, TaskService>();
    builder!.Services.AddTransient<IImageService, ImageService>();
    builder!.Services.AddTransient<IFileService, FileService>();
    builder!.Services.AddTransient<IManagementService, ManagementService>();
    builder!.Services.AddTransient<ITypelistService, TypelistService>();
}

void SetupServerAppServices()
{
    builder!.Services.AddTransient<IResourceAppService, ResourceAppService>();
    builder!.Services.AddTransient<ICompanyAppService, CompanyAppService>();
    builder!.Services.AddTransient<IUserAppService, UserAppService>();
    builder!.Services.AddTransient<IFileAppService, FileAppService>();
    builder!.Services.AddTransient<ITaskAppService, TaskAppService>();
    builder!.Services.AddTransient<IManagementAppService, ManagementAppService>();
    builder!.Services.AddTransient<ITypelistAppService, TypelistAppService>();
}

void SetupServerHelperServices()
{
    builder!.Services.AddSingleton<IMemoryCache, MemoryCache>();
    builder!.Services.AddSingleton<IResourceHandler, ResourceHandler>();
    builder!.Services.AddTransient<ILoggingService, SentryLoggingService>();
}

void SetupServerHttpClients()
{
    builder!.Services.AddHttpClient<ITaskCoreService, TaskCoreService>();
}

void SetupServerJobServices()
{
    builder!.Services.AddTransient<UpdateTaskStatusesJob>();
    builder!.Services.AddTransient<UpdateTaskDetailsJob>();
}

namespace FoP_IMT.Server
{
    /// <summary>
    /// Public server program class for tests + data generation purposes
    /// </summary>
    public partial class Program { }
}
