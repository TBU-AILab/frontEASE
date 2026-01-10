using FrontEASE.DataGenerator.Infrastructure.Attributes;
using FrontEASE.Infrastructure.Data;

namespace FrontEASE.DataGenerator.Services.Erasers.Resources
{
    [Order(0)]
    public class ResourceEraser(ApplicationDbContext dataContext) : IEraser
    {
        public async Task Erase()
        {
            dataContext.Resources.RemoveRange(dataContext.Resources);
            await dataContext.SaveChangesAsync();
        }
    }
}
