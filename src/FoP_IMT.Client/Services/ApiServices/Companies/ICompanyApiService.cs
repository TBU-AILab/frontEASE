using FoP_IMT.Shared.Data.DTOs.Companies;

namespace FoP_IMT.Client.Services.ApiServices.Companies
{
    public interface ICompanyApiService
    {
        Task<IList<CompanyDto>> LoadCompanies();
        Task<CompanyDto?> AddCompany(CompanyDto addCompanyDto);
        Task<CompanyDto?> UpdateCompany(CompanyDto editCompanyDto);
        Task<bool> DeleteCompany(Guid companyID);
    }
}
