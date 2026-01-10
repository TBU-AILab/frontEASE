using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Infrastructure.Data;

namespace FrontEASE.DataGenerator.Services.Erasers.Resources
{
    [Order(1)]
    public class CountryCodeEraser(ApplicationDbContext dataContext) : IEraser
    {
        public async Task Erase()
        {
            dataContext.CountryCodes.RemoveRange(dataContext.CountryCodes);
            await dataContext.SaveChangesAsync();
        }
    }
}
