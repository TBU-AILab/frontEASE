using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Infrastructure.Data;

namespace FrontEASE.DataGenerator.Services.Erasers.Resources
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
