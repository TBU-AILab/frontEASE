using AutoMapper;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Companies;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Domain.Services.Shared.Images;
using FrontEASE.Shared.Data.Enums.Shared.Images;

namespace FrontEASE.Domain.Services.Companies
{
    public class CompanyService : ICompanyService
    {
        private readonly IImageService _imageService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CompanyService(
            IImageService imageService,
            ICompanyRepository companyRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _imageService = imageService;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Company> Create(Company company)
        {
            company.ID = Guid.NewGuid();
            if (!string.IsNullOrEmpty(company?.Image?.ImageData))
            {
                company!.Image!.Type = ImageType.COMPANY_LOGO;
                await _imageService.SaveImage(company!.Image!, company.ID);
            }
            else
            {
                if (string.IsNullOrEmpty(company?.Image?.ImageUrl))
                {
                    company!.Image = null;
                }
            }

            var inserted = await _companyRepository.Insert(company);
            return inserted;
        }

        public async Task<Company> Load(Guid id)
        {
            var company = await _companyRepository.Load(id) ?? throw new NotFoundException();
            return company;
        }

        public async Task<IList<Company>> LoadAll()
        {
            var companies = await _companyRepository.LoadAll();
            return companies;
        }

        public async Task<Company> Update(Company company)
        {
            var updated = await Load(company.ID);
            var linkedUserIDs = company.Users.Select(x => x.Id);
            var linkedUsers = await _userRepository.LoadWhere(x => linkedUserIDs.Contains(x.Id));

            if (!string.IsNullOrWhiteSpace(company.Image?.ImageData) && !string.IsNullOrWhiteSpace(updated.Image?.ImageUrl))
            { _imageService.DeleteImage(updated.Image!); }

            _mapper.Map(company, updated);
            updated.Users.Clear();
            foreach (var user in linkedUsers) { updated.Users.Add(user); }

            if (!string.IsNullOrEmpty(updated?.Image?.ImageData))
            {
                updated!.Image!.Type = ImageType.COMPANY_LOGO;
                await _imageService.SaveImage(updated!.Image!, company.ID);
            }
            else
            {
                if (string.IsNullOrEmpty(updated?.Image?.ImageUrl))
                {
                    updated!.Image = null;
                }
            }

            updated = await _companyRepository.Update(updated);
            return updated;
        }

        public async Task Delete(Company company)
        {
            await _companyRepository.Delete(company);
            if (!string.IsNullOrEmpty(company?.Image?.ImageUrl))
            { _imageService.DeleteImage(company?.Image!); }
        }
    }
}
