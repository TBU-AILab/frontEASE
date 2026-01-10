using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Infrastructure.Data;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Resources.Defaults;

namespace FrontEASE.DataGenerator.Services.Generators.Resources
{
    [Order(1)]
    public class ResourceGenerator(ApplicationDbContext dataContext) : IGenerator
    {
        public async Task Generate()
        {
            var resources = DefaultResourcesConfiguration.GetDefaults();

            await dataContext.Resources.AddRangeAsync(resources);
            await dataContext.SaveChangesAsync();
        }
    }
}
