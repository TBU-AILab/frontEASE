using Microsoft.EntityFrameworkCore.Storage;

namespace FoP_IMT.Domain.Repositories
{
    public interface IRepository
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
