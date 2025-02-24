using AutoMapper;
using FoP_IMT.Domain.Entities.Companies;
using FoP_IMT.Domain.Services.Companies;
using FoP_IMT.Shared.Data.DTOs.Companies;

namespace FoP_IMT.Application.AppServices.Companies
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

        public async Task<CompanyDto> Create(CompanyDto company)
        {
            var insertedEntity = _mapper.Map<Company>(company);
            insertedEntity = await _companyService.Create(insertedEntity);

            var insertedDto = _mapper.Map<CompanyDto>(insertedEntity);
            return insertedDto;
        }

        public async Task Delete(Guid id)
        {
            var deletedEntity = await _companyService.Load(id);
            await _companyService.Delete(deletedEntity);
        }

        public async Task<CompanyDto> Load(Guid id)
        {
            var companyEntity = await _companyService.Load(id);
            var companyDto = _mapper.Map<CompanyDto>(companyEntity);
            return companyDto;
        }

        public async Task<IList<CompanyDto>> LoadAll()
        {
            var companyEntities = await _companyService.LoadAll();
            var companyDtos = _mapper.Map<IList<CompanyDto>>(companyEntities);
            return companyDtos;
        }

        public async Task<CompanyDto> Update(CompanyDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            companyEntity = await _companyService.Update(companyEntity);
            var updatedDto = _mapper.Map<CompanyDto>(companyEntity);
            return updatedDto;
        }
    }
}
