using FrontEASE.DataGenerator.Infrastructure.Seeding;
using FrontEASE.DataGenerator.Services.Erasers;
using FrontEASE.DataGenerator.Services.Generators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FrontEASE.DataGenerator
{

    public class DataGeneratorApp(IDictionary<string, string>? additionalSettings = null) : WebApplicationFactory<Server.Program>
    {
        private readonly string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (additionalSettings is not null)
            {
                foreach (var additionalSetting in additionalSettings)
                {
                    builder.UseSetting(additionalSetting.Key, additionalSetting.Value);
                }
            }
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);
            return base.CreateHost(builder.ConfigureServices(services =>
            {
                services.AddTransient<IDataSeeder, DataSeeder>();

                RegisterServicesByInterface<IEraser>(services);
                RegisterServicesByInterface<IGenerator>(services);
            }));
        }

        static void RegisterServicesByInterface<TInterface>(IServiceCollection services)
        {
            var interfaceType = typeof(TInterface);
            var implementations = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            foreach (var implementation in implementations)
            {
                services.AddTransient(interfaceType, implementation);
            }
        }
    }
}