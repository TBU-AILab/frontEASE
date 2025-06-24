using AutoMapper;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Companies;
using FrontEASE.Domain.Repositories.Users;
using FrontEASE.Domain.Services.Shared.Images;
using FrontEASE.Shared.Data.Enums.Shared.Images;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;

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

        public async Task<Company> Create(Company company, CancellationToken cancellationToken)
        {
            company.ID = Guid.NewGuid();
            if (!string.IsNullOrEmpty(company?.Image?.ImageData))
            {
                company!.Image!.Type = ImageType.COMPANY_LOGO;
                await _imageService.SaveImage(company!.Image!, company.ID, cancellationToken);
            }
            else
            {
                if (string.IsNullOrEmpty(company?.Image?.ImageUrl))
                {
                    company!.Image = null;
                }
            }

            var inserted = await _companyRepository.Insert(company, cancellationToken);
            return inserted;
        }

        public async Task<Company> Load(Guid id, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.Load(id, cancellationToken) ?? throw new NotFoundException();
            return company;
        }

        public async Task<IList<Company>> LoadAll(CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.LoadAll(cancellationToken);
            return companies;
        }

        public async Task<Company> Update(Company company, CancellationToken cancellationToken)
        {
            var updated = await Load(company.ID, cancellationToken);
            var linkedUserIDs = company.Users.Select(x => x.Id);
            var linkedUsers = await _userRepository.LoadWhere(x => linkedUserIDs.Contains(x.Id), cancellationToken);

            if (!string.IsNullOrWhiteSpace(company.Image?.ImageData) && !string.IsNullOrWhiteSpace(updated.Image?.ImageUrl))
            { _imageService.DeleteImage(updated.Image!); }

            _mapper.Map(company, updated);
            updated.Users.Clear();
            updated.Users.AddRange(linkedUsers);

            if (!string.IsNullOrEmpty(updated?.Image?.ImageData))
            {
                updated!.Image!.Type = ImageType.COMPANY_LOGO;
                await _imageService.SaveImage(updated!.Image!, company.ID, cancellationToken);
            }
            else
            {
                if (string.IsNullOrEmpty(updated?.Image?.ImageUrl))
                {
                    updated!.Image = null;
                }
            }

            updated = await _companyRepository.Update(updated, cancellationToken);
            return updated;
        }

        public async Task Delete(Company company, CancellationToken cancellationToken)
        {
            await _companyRepository.Delete(company, cancellationToken);
            if (!string.IsNullOrEmpty(company?.Image?.ImageUrl))
            { _imageService.DeleteImage(company?.Image!); }
        }
    }
}
