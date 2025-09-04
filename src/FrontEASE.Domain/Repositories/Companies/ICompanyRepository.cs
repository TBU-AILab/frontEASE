using FrontEASE.Domain.Entities.Companies;
using System.Linq.Expressions;

namespace FrontEASE.Domain.Repositories.Companies
{
    public interface ICompanyRepository : IRepository
    {
        Task<Company?> Load(Guid id, CancellationToken cancellationToken);
        Task<IList<Company>> LoadAll(CancellationToken cancellationToken);
        Task<IList<Company>> LoadWhere(Expression<Func<Company, bool>> predicate, CancellationToken cancellationToken);
        Task<Company> Insert(Company company, CancellationToken cancellationToken);
        Task<Company> Update(Company company, CancellationToken cancellationToken);
        Task Delete(Company company, CancellationToken cancellationToken);
    }
}
