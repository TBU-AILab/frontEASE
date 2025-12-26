using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Shared.Addresses;
using FrontEASE.Shared.Data.DTOs.Shared.Images;
using FrontEASE.Shared.Data.DTOs.Shared.Users;

namespace FrontEASE.Client.Services.ModelManipulationServices.Companies
{
    public class CompanyManipulationService(IMapper mapper) : ICompanyManipulationService
    {
        public void CleanAddressInfo(CompanyDto company) => company.Address = null;
        public void InitAddressInfo(CompanyDto company) => company.Address = new();
        public void CleanUsersInfo(CompanyDto company) => company.Users.Clear();

        public void ConsolidateCompanyModel(CompanyDto company)
        {
            if (string.IsNullOrWhiteSpace(company.Image?.ImageData) && string.IsNullOrWhiteSpace(company.Image?.ImageUrl))
            {
                company.Image = null;
            }
        }

        public void ReinitializeModel(CompanyDto company)
        {
            var cleanModel = new CompanyDto()
            {
                Address = new AddressDto(),
                Image = new ImageDto()
            };
            mapper.Map(cleanModel, company);
        }

        public void SortUsersToCompanies(IList<ApplicationUserDto> users, IList<CompanyDto> companies)
        {
            foreach (var company in companies)
            {
                var relevantUserIDs = company.Users.Select(x => x.Id);
                var relevantUsers = users.Where(x => relevantUserIDs.Contains(x.Id));

                company.Users.Clear();
                foreach (var user in relevantUsers) { company.Users.Add(user); }
            }
        }
    }
}
