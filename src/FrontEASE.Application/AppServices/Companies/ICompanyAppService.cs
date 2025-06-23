using FrontEASE.Shared.Data.DTOs.Companies;

namespace FrontEASE.Application.AppServices.Companies
{
    public interface ICompanyAppService
    {
        Task<CompanyDto> Create(CompanyDto company, CancellationToken cancellationToken);
        Task<CompanyDto> Load(Guid id, CancellationToken cancellationToken);
        Task<IList<CompanyDto>> LoadAll(CancellationToken cancellationToken);
        Task<CompanyDto> Update(CompanyDto company, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
