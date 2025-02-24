using FoP_IMT.DataGenerator.Infrastructure.Attributes;
using FoP_IMT.Infrastructure.Data;

namespace FoP_IMT.DataGenerator.Services.Erasers.Resources
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
