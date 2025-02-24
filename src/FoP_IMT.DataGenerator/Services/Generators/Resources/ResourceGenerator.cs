using FoP_IMT.DataGenerator.Infrastructure.Attributes;
using FoP_IMT.Infrastructure.Data;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.Resources.Defaults;

namespace FoP_IMT.DataGenerator.Services.Generators.Resources
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
