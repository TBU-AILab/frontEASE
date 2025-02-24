using FoP_IMT.Domain.Entities.Shared.Resources;
using FoP_IMT.Domain.Repositories.Shared.Resources;
using FoP_IMT.Infrastructure.Data;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FoP_IMT.Infrastructure.Repositories.Shared.Resources
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
