using FoP_IMT.DataGenerator.Infrastructure.Attributes;
using FoP_IMT.Infrastructure.Data;
using FoP_IMT.Infrastructure.Data.Configuration.Shared.CountryCodes.Defaults;

namespace FoP_IMT.DataGenerator.Services.Generators.Resources
{
    [Order(0)]
    public class CountryCodeGenerator : IGenerator
    {
        private readonly ApplicationDbContext _dataContext;
        public CountryCodeGenerator(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Generate()
        {
            var countryCodes = DefaultCountryCodesConfiguration.GetDefaults().ToList();

            await _dataContext.CountryCodes.AddRangeAsync(countryCodes);
            await _dataContext.SaveChangesAsync();
        }
    }
}
