using Microsoft.EntityFrameworkCore.Storage;

namespace FrontEASE.Domain.Repositories
{
    public interface IRepository
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
