using AutoMapper;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Services.Companies;
using FrontEASE.Shared.Data.DTOs.Companies;

namespace FrontEASE.Application.AppServices.Companies
{
    public class CompanyAppService : ICompanyAppService
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyAppService(
            ICompanyService companyService,
            IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Create(CompanyDto company, CancellationToken cancellationToken)
        {
            var insertedEntity = _mapper.Map<Company>(company);
            insertedEntity = await _companyService.Create(insertedEntity, cancellationToken);

            var insertedDto = _mapper.Map<CompanyDto>(insertedEntity);
            return insertedDto;
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var deletedEntity = await _companyService.Load(id, cancellationToken);
            await _companyService.Delete(deletedEntity, cancellationToken);
        }

        public async Task<CompanyDto> Load(Guid id, CancellationToken cancellationToken)
        {
            var companyEntity = await _companyService.Load(id, cancellationToken);
            var companyDto = _mapper.Map<CompanyDto>(companyEntity);
            return companyDto;
        }

        public async Task<IList<CompanyDto>> LoadAll(CancellationToken cancellationToken)
        {
            var companyEntities = await _companyService.LoadAll(cancellationToken);
            var companyDtos = _mapper.Map<IList<CompanyDto>>(companyEntities);
            return companyDtos;
        }

        public async Task<CompanyDto> Update(CompanyDto company, CancellationToken cancellationToken)
        {
            var companyEntity = _mapper.Map<Company>(company);

            companyEntity = await _companyService.Update(companyEntity, cancellationToken);
            var updatedDto = _mapper.Map<CompanyDto>(companyEntity);
            return updatedDto;
        }
    }
}
