using FoP_IMT.DataGenerator.Infrastructure.Attributes;
using FoP_IMT.Infrastructure.Data;

namespace FoP_IMT.DataGenerator.Services.Erasers.Resources
{
    [Order(0)]
    public class ResourceEraser : IEraser
    {
        private readonly ApplicationDbContext _dataContext;
        public ResourceEraser(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Erase()
        {
            _dataContext.Resources.RemoveRange(_dataContext.Resources);
            await _dataContext.SaveChangesAsync();
        }
    }
}
