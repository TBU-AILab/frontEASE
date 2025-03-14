using FrontEASE.Domain.Entities.Companies;
using System.Linq.Expressions;

namespace FrontEASE.Domain.Repositories.Companies
{
    public interface ICompanyRepository : IRepository
    {
        Task<Company?> Load(Guid id);
        Task<IList<Company>> LoadAll();
        Task<IList<Company>> LoadWhere(Expression<Func<Company, bool>> predicate);
        Task<Company> Insert(Company company);
        Task<Company> Update(Company company);
        Task Delete(Company company);
    }
}
