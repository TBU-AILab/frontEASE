using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Infrastructure.Data;
using FrontEASE.Infrastructure.Data.Configuration.Shared.CountryCodes.Defaults;

namespace FrontEASE.DataGenerator.Services.Generators.Resources
{
    [Order(0)]
    public class CountryCodeGenerator(ApplicationDbContext dataContext) : IGenerator
    {
        public async Task Generate()
        {
            var countryCodes = DefaultCountryCodesConfiguration.GetDefaults();

            await dataContext.CountryCodes.AddRangeAsync(countryCodes);
            await dataContext.SaveChangesAsync();
        }
    }
}
