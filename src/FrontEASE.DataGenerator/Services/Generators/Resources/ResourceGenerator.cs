using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Infrastructure.Data;
using FrontEASE.Infrastructure.Data.Configuration.Shared.Resources.Defaults;

namespace FrontEASE.DataGenerator.Services.Generators.Resources
{
    [Order(1)]
    public class ResourceGenerator : IGenerator
    {
        private readonly ApplicationDbContext _dataContext;
        public ResourceGenerator(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Generate()
        {
            var resources = DefaultResourcesConfiguration.GetDefaults().ToList();

            await _dataContext.Resources.AddRangeAsync(resources);
            await _dataContext.SaveChangesAsync();
        }
    }
}
