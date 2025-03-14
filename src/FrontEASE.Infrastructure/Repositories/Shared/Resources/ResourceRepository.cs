using FrontEASE.Domain.Entities.Shared.Resources;
using FrontEASE.Domain.Repositories.Shared.Resources;
using FrontEASE.Infrastructure.Data;
using FrontEASE.Shared.Data.Enums.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FrontEASE.Infrastructure.Repositories.Shared.Resources
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _context;

        public ResourceRepository(ApplicationDbContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<Resource?> Load(LanguageCode language, string resourceCode)
        {
            var resource = await _context.Resources
                .AsNoTracking()
                .Where(x =>
                    x.CountryCodeID == language &&
                    x.ResourceCode == resourceCode)
                .SingleOrDefaultAsync();

            return resource;
        }

        public async Task<IList<Resource>> LoadAll(LanguageCode language)
        {
            var resources = await _context.Resources
                .AsNoTracking()
                .Where(x => x.CountryCodeID == language)
            .ToListAsync() ?? [];
            return resources;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.BeginTransactionAsync(cancellationToken);
    }
}
