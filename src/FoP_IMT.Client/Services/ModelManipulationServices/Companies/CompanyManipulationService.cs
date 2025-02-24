using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Companies;
using FoP_IMT.Shared.Data.DTOs.Shared.Addresses;
using FoP_IMT.Shared.Data.DTOs.Shared.Images;
using FoP_IMT.Shared.Data.DTOs.Shared.Users;

namespace FoP_IMT.Client.Services.ModelManipulationServices.Companies
{
    public class CompanyManipulationService : ICompanyManipulationService
    {
        private readonly IMapper _mapper;

        public CompanyManipulationService(IMapper mapper)
        {
            _mapper = mapper;
        }

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
            _mapper.Map(cleanModel, company);
        }

        public void SortUsersToCompanies(IList<ApplicationUserDto> users, IList<CompanyDto> companies)
        {
            foreach (var company in companies)
            {
                var relevantUserIDs = company.Users.Select(x => x.Id).ToList();
                var relevantUsers = users.Where(x => relevantUserIDs.Contains(x.Id)).ToList();

                company.Users.Clear();
                foreach (var user in relevantUsers) { company.Users.Add(user); }
            }
        }
    }
}
