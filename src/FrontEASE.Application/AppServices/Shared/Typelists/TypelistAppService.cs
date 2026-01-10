using AutoMapper;
using FrontEASE.Domain.Services.Shared.Typelists;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FrontEASE.Application.AppServices.Shared.Typelists
{
    public class TypelistAppService(IMapper mapper, ITypelistService typelistService) : ITypelistAppService
    {
        public async Task<IList<TaskModuleNoValidationDto>> LoadModuleTypes(CancellationToken cancellationToken)
        {
            var types = await typelistService.LoadModuleTypes(false, cancellationToken);
            var typeDtos = mapper.Map<IList<TaskModuleNoValidationDto>>(types);

            return typeDtos;
        }
    }
}
