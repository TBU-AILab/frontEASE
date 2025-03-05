using FrontEASE.Domain.Entities.Companies;

namespace FrontEASE.Domain.Services.Companies
{
    public interface ICompanyService
    {
        Task<Company> Load(Guid id);
        Task<IList<Company>> LoadAll();
        Task<Company> Create(Company company);
        Task<Company> Update(Company company);
        Task Delete(Company company);
    }
}
