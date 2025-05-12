using AutoMapper;
using FrontEASE.Application.AppServices.Companies;
using FrontEASE.Application.AppServices.Files;
using FrontEASE.Application.AppServices.Management;
using FrontEASE.Application.AppServices.Shared.Core;
using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Application.AppServices.Shared.Typelists;
using FrontEASE.Application.AppServices.Tasks;
using FrontEASE.Application.AppServices.Users;
using FrontEASE.Application.Infrastructure.Jobs;
using FrontEASE.Application.Infrastructure.Jobs.Tasks;
using FrontEASE.Application.Infrastructure.Mappings.Companies;
using FrontEASE.Application.Infrastructure.Mappings.Management;
using FrontEASE.Application.Infrastructure.Mappings.Management.Core;
using FrontEASE.Application.Infrastructure.Mappings.Management.General;
using FrontEASE.Application.Infrastructure.Mappings.Management.Tokens;
using FrontEASE.Application.Infrastructure.Mappings.Management.Tokens.Connectors;
using FrontEASE.Application.Infrastructure.Mappings.Shared.Addresses;
using FrontEASE.Application.Infrastructure.Mappings.Shared.Files;
using FrontEASE.Application.Infrastructure.Mappings.Shared.Images;
using FrontEASE.Application.Infrastructure.Mappings.Shared.Resources;
using FrontEASE.Application.Infrastructure.Mappings.Tasks;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Actions.Requests;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.Enum;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.List;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.RepeatedMessage;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Messages;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Shared;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Solutions;
using FrontEASE.DataContracts.Models.Core;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Infrastructure.Settings.Connection;
using FrontEASE.Domain.Repositories.Companies;
using FrontEASE.Domain.Repositories.Jobs;
using FrontEASE.Domain.Repositories.Management;
using FrontEASE.Domain.Repositories.Shared.Resources;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Domain.Services.Companies;
using FrontEASE.Domain.Services.Core;
using FrontEASE.Domain.Services.Core.Connector;
using FrontEASE.Domain.Services.Management;
using FrontEASE.Domain.Services.Shared.Files;
using FrontEASE.Domain.Services.Shared.Images;
using FrontEASE.Domain.Services.Shared.Logging;
using FrontEASE.Domain.Services.Shared.Logging.Sentry;
using FrontEASE.Domain.Services.Shared.Resources;
using FrontEASE.Domain.Services.Shared.Typelists;
using FrontEASE.Domain.Services.Tasks;
using FrontEASE.Domain.Services.Users;
using FrontEASE.Infrastructure.Data;
using FrontEASE.Infrastructure.HealthChecks;
using FrontEASE.Infrastructure.Repositories.Companies;
using FrontEASE.Infrastructure.Repositories.Jobs;
using FrontEASE.Infrastructure.Repositories.Management;
using FrontEASE.Infrastructure.Repositories.Shared.Resources;
using FrontEASE.Infrastructure.Repositories.Tasks;
using FrontEASE.Infrastructure.Repositories.Users;
using FrontEASE.Server.Infrastructure.Hangfire.Attributes;
using FrontEASE.Server.Infrastructure.Overrides.Auth.Models;
using FrontEASE.Server.Infrastructure.Swagger.Filters.Documentation;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions;
using FrontEASE.Shared.Services.Resources;
using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Security.Claims;

var executingAssembly = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);
var settings = (AppSettings?)null;
var connection = (ConnectionStrings?)null;

var isGeneratorRun = executingAssembly?.Location?.Contains("DataGenerator") ?? false;

/* Basic */
SetupArchitecture();

/* Global*/
SetupDatabase();
SetupJobs();
SetupMvc();
SetupAuth();
SetupSwaggerGen();
SetupMonitoring();

/* Application */
SetupRepositories();
SetupServices();
SetupAppServices();
SetupHelperServices();
SetupHttpClients();
SetupMappings();
SetupJobServices();

/* BUILD APP */
var app = builder.Build();

/* Runtime */
ConfigureArchitecture();
ConfigureSwagger();
ConfigureJobs();
ConfigureMonitoring();
ConfigureAuth();
ConfigureFramework();

await app.RunAsync();

#region Configurations

void ConfigureAuth()
{
    app.UseCors("AllowAll");

    var identityOptions = new IdentityApiEndpointRouteBuilderOptions()
    {
        ExcludeRegisterPost = true,
        ExcludeLoginPost = false,
        ExcludeRefreshPost = true,
        ExcludeConfirmEmailGet = true,
        ExcludeResendConfirmationEmailPost = true,
        ExcludeForgotPasswordPost = true,
        ExcludeResetPasswordPost = true,
        Exclude2faPost = true,
        ExcludeInfoPost = true,
        ExcludeInfoGet = false
    };

    app.MapIdentityApiFilterable<ApplicationUser>(identityOptions);
    app.UseHttpsRedirection();
    app.MapStaticAssets();
    app.UseAuthorization();
}

void ConfigureFramework()
{
    app.MapControllers();

    var fordwardedHeaderOptions = new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    };
    fordwardedHeaderOptions.KnownNetworks.Clear();
    fordwardedHeaderOptions.KnownProxies.Clear();

    app.UseForwardedHeaders(fordwardedHeaderOptions);
}


void ConfigureMonitoring()
{
    if (settings?.SentrySettings?.IsEnabled == true)
    {
        app.UseCors("AllowSentryTrace");
        app.UseSentryTracing();
    }
}

void ConfigureJobs()
{
    if (settings!.HangfireSettings?.IsEnabled == true && !isGeneratorRun)
    {
        var options = new DashboardOptions
        {
            AppPath = null,
            Authorization = [
            new BasicAuthAuthorizationFilter(
                new BasicAuthAuthorizationFilterOptions
                {
                    RequireSsl = false,
                    SslRedirect = false,
                    LoginCaseSensitive = true,
                    Users =
                    [
                        new BasicAuthAuthorizationUser
                        {
                            Login = settings.HangfireSettings.Login!.Username,
                            PasswordClear = settings.HangfireSettings.Login!.Password,
                        },
                    ]
                })
            ],
            DashboardTitle = "EASE - Jobs",
        };

        app.UseHangfireDashboard("/Hangfire", options);
        GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());
        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() { Attempts = 3 });

        JobFactory.ScheduleJobs(settings!, app.Services);
    }
}

void ConfigureSwagger()
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/Swagger/v1/swagger.json", "API - FrontEASE");
    });
}

void ConfigureArchitecture()
{
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseHsts();
    }

    app.UseHealthChecks("/api/health");
}

void SetupMvc()
{
    builder.Services.AddControllers();
}

void SetupAuth()
{
    builder.Services.AddAuthorization();
    builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
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

        builder.Services.AddHangfireServer();
    }
}

void SetupArchitecture()
{
    settings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
    connection = builder.Configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

    builder.Services.AddSingleton(settings!);
    builder.Services.AddSingleton(connection!);

    builder.Services.AddHealthChecks().AddCheck<ConnectivityHealthCheck>("Connectivity");
    builder.Services.AddSingleton(TimeProvider.System);
}


void SetupMonitoring()
{
    if (settings?.SentrySettings?.IsEnabled == true)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .WithHeaders("Content-Type", "Authorization", "sentry-trace", "baggage"); // Add any other needed headers
            });
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
                    return context.TransactionContext.IsParentSampled.Value ? 1.0 : 0.0;
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
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "FrontEASE - Effortless Algorithmic Solution Evolution (FE)", Version = DateTime.UtcNow.ToString("dd.MM.yyyy") });
        options.DescribeAllParametersInCamelCase();
        options.SupportNonNullableReferenceTypes();

        options.OperationFilter<AuthResponsesFilter>();
        options.OperationFilter<FromQueryDictionaryFilter>();
        options.SchemaFilter<EnumSchemaFilter>();

        AddIntegrationAPIDtos(options);

        var serverAssemblyDoc = $"{Assembly.GetAssembly(typeof(Program))!.GetName().Name}.xml";
        var sharedAssemblyDoc = $"{Assembly.GetAssembly(typeof(StatusResultDto))!.GetName().Name}.xml";
        var dataContractsAssemblyDoc = $"{Assembly.GetAssembly(typeof(ICoreDto))!.GetName().Name}.xml";
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
    var taskCoreDtoTypes = typeof(ICoreDto).Assembly.GetTypes().Where(t => typeof(ICoreDto).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
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

void SetupMappings()
{
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new FileMappingProfile());
        mc.AddProfile(new ResourceMappingProfile());
        mc.AddProfile(new ImageMappingProfile());
        mc.AddProfile(new AddressMappingProfile());
        mc.AddProfile(new CompanyMappingProfile());
        mc.AddProfile(new UserMappingProfile());

        mc.AddProfile(new UserPreferencesMappingProfile());
        mc.AddProfile(new UserPreferencesTokenOptionMappingProfile());
        mc.AddProfile(new UserPreferencesGeneralOptionsMappingProfile());
        mc.AddProfile(new UserPreferencesTokenOptionConnectorMappingProfile());
        mc.AddProfile(new GlobalPreferencesMappingProfile());
        mc.AddProfile(new CorePackageMappingProfile());
        mc.AddProfile(new CoreModuleMappingProfile());

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
        mc.AddProfile(new TaskModuleParameterListOptionMappingProfile());
        mc.AddProfile(new TaskModuleParameterListOptionParamsMappingProfile());

        mc.AddProfile(new TaskFilterActionRequestMappingProfile());
    });

    IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
}

void SetupRepositories()
{
    builder!.Services.AddTransient<IResourceRepository, ResourceRepository>();
    builder!.Services.AddTransient<ICompanyRepository, CompanyRepository>();
    builder!.Services.AddTransient<IUserRepository, UserRepository>();
    builder!.Services.AddTransient<ITaskRepository, TaskRepository>();
    builder!.Services.AddTransient<IManagementRepository, ManagementRepository>();
    builder!.Services.AddTransient<IJobLogRepository, JobLogRepository>();
}

void SetupServices()
{
    builder!.Services.AddTransient<IResourceService, ResourceService>();
    builder!.Services.AddTransient<ICompanyService, CompanyService>();
    builder!.Services.AddTransient<IUserService, UserService>();
    builder!.Services.AddTransient<ITaskService, TaskService>();
    builder!.Services.AddTransient<IImageService, ImageService>();
    builder!.Services.AddTransient<IFileService, FileService>();
    builder!.Services.AddTransient<IManagementService, ManagementService>();
    builder!.Services.AddTransient<ITypelistService, TypelistService>();
    builder!.Services.AddTransient<ICoreService, CoreService>();
}

void SetupAppServices()
{
    builder!.Services.AddTransient<IResourceAppService, ResourceAppService>();
    builder!.Services.AddTransient<ICompanyAppService, CompanyAppService>();
    builder!.Services.AddTransient<IUserAppService, UserAppService>();
    builder!.Services.AddTransient<IFileAppService, FileAppService>();
    builder!.Services.AddTransient<ITaskAppService, TaskAppService>();
    builder!.Services.AddTransient<IManagementAppService, ManagementAppService>();
    builder!.Services.AddTransient<ITypelistAppService, TypelistAppService>();
    builder!.Services.AddTransient<ICoreAppService, CoreAppService>();
}

void SetupHelperServices()
{
    builder!.Services.AddSingleton<IMemoryCache, MemoryCache>();
    builder!.Services.AddSingleton<IResourceHandler, ResourceHandler>();
    builder!.Services.AddTransient<ILoggingService, SentryLoggingService>();
}

void SetupHttpClients()
{
    builder!.Services.AddHttpClient<ICoreConnector, CoreConnector>();
}

void SetupJobServices()
{
    builder!.Services.AddTransient<UpdateTaskStatusesJob>();
    builder!.Services.AddTransient<UpdateTaskDetailsJob>();
    builder!.Services.AddTransient<InitialTaskSyncJob>();
}

#endregion

namespace FrontEASE.Server
{
    /// <summary>
    /// Public server program class for tests + data generation purposes
    /// </summary>
    public partial class Program { }
}