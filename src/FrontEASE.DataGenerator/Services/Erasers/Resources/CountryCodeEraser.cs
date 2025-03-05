using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Infrastructure.Data;

namespace FrontEASE.DataGenerator.Services.Erasers.Resources
{
    [Order(1)]
    public class CountryCodeEraser : IEraser
    {
        private readonly ApplicationDbContext _dataContext;
        public CountryCodeEraser(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Erase()
        {
            _dataContext.CountryCodes.RemoveRange(_dataContext.CountryCodes);
            await _dataContext.SaveChangesAsync();
        }
    }
}
