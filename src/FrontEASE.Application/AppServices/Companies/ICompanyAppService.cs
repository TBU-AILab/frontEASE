using FrontEASE.Shared.Data.DTOs.Companies;

namespace FrontEASE.Application.AppServices.Companies
{
    public interface ICompanyAppService
    {
        Task<CompanyDto> Create(CompanyDto company);
        Task<CompanyDto> Load(Guid id);
        Task<IList<CompanyDto>> LoadAll();
        Task<CompanyDto> Update(CompanyDto company);
        Task Delete(Guid id);
    }
}
