using FoP_IMT.DataGenerator.Infrastructure.Seeding;
using FoP_IMT.DataGenerator.Services.Erasers;
using FoP_IMT.DataGenerator.Services.Generators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoP_IMT.DataGenerator
{

    public class DataGeneratorApp : WebApplicationFactory<Server.Program>
    {
        private readonly string _environment;
        private readonly Dictionary<string, string>? _additionalSettings;

        public DataGeneratorApp(string environment = "Development", Dictionary<string, string>? additionalSettings = null)
        {
            _environment = environment;
            _additionalSettings = additionalSettings;
        }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (_additionalSettings is not null)
            {
                foreach (var additionalSetting in _additionalSettings)
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