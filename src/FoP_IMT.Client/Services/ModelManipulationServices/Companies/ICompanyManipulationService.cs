using FoP_IMT.Shared.Data.DTOs.Companies;
using FoP_IMT.Shared.Data.DTOs.Shared.Users;

namespace FoP_IMT.Client.Services.ModelManipulationServices.Companies
{
    public interface ICompanyManipulationService
    {
        void CleanAddressInfo(CompanyDto company);
        void InitAddressInfo(CompanyDto company);
        void CleanUsersInfo(CompanyDto company);
        void ConsolidateCompanyModel(CompanyDto company);
        void ReinitializeModel(CompanyDto company);
        void SortUsersToCompanies(IList<ApplicationUserDto> users, IList<CompanyDto> companies);
    }
}
