using FrontEASE.Shared.Data.DTOs.Companies;

namespace FrontEASE.Client.Services.ApiServices.Companies
{
    public interface ICompanyApiService
    {
        Task<IList<CompanyDto>> LoadCompanies();
        Task<CompanyDto?> AddCompany(CompanyDto addCompanyDto);
        Task<CompanyDto?> UpdateCompany(CompanyDto editCompanyDto);
        Task<bool> DeleteCompany(Guid companyID);
    }
}
