using AutoMapper;
using FrontEASE.Domain.Entities.Companies;
using FrontEASE.Domain.Services.Companies;
using FrontEASE.Shared.Data.DTOs.Companies;

namespace FrontEASE.Application.AppServices.Companies
{
    public class CompanyAppService(
        ICompanyService companyService,
        IMapper mapper) : ICompanyAppService
    {
        public async Task<CompanyDto> Create(CompanyDto company, CancellationToken cancellationToken)
        {
            var insertedEntity = mapper.Map<Company>(company);
            insertedEntity = await companyService.Create(insertedEntity, cancellationToken);

            var insertedDto = mapper.Map<CompanyDto>(insertedEntity);
            return insertedDto;
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var deletedEntity = await companyService.Load(id, cancellationToken);
            await companyService.Delete(deletedEntity, cancellationToken);
        }

        public async Task<CompanyDto> Load(Guid id, CancellationToken cancellationToken)
        {
            var companyEntity = await companyService.Load(id, cancellationToken);
            var companyDto = mapper.Map<CompanyDto>(companyEntity);
            return companyDto;
        }

        public async Task<IList<CompanyDto>> LoadAll(CancellationToken cancellationToken)
        {
            var companyEntities = await companyService.LoadAll(cancellationToken);
            var companyDtos = mapper.Map<IList<CompanyDto>>(companyEntities);
            return companyDtos;
        }

        public async Task<CompanyDto> Update(CompanyDto company, CancellationToken cancellationToken)
        {
            var companyEntity = mapper.Map<Company>(company);

            companyEntity = await companyService.Update(companyEntity, cancellationToken);
            var updatedDto = mapper.Map<CompanyDto>(companyEntity);
            return updatedDto;
        }
    }
}
