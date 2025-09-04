using FrontEASE.Domain.Entities.Companies;

namespace FrontEASE.Domain.Services.Companies
{
    public interface ICompanyService
    {
        Task<Company> Load(Guid id, CancellationToken cancellationToken);
        Task<IList<Company>> LoadAll(CancellationToken cancellationToken);
        Task<Company> Create(Company company, CancellationToken cancellationToken);
        Task<Company> Update(Company company, CancellationToken cancellationToken);
        Task Delete(Company company, CancellationToken cancellationToken);
    }
}
