using AutoMapper;
using FrontEASE.Domain.Services.Shared.Typelists;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FrontEASE.Application.AppServices.Shared.Typelists
{
    public class TypelistAppService : ITypelistAppService
    {
        private readonly IMapper _mapper;
        private readonly ITypelistService _typelistService;

        public TypelistAppService(IMapper mapper, ITypelistService typelistService)
        {
            _mapper = mapper;
            _typelistService = typelistService;
        }

        public async Task<IList<TaskModuleNoValidationDto>> LoadModuleTypes()
        {
            var types = await _typelistService.LoadModuleTypes(false);
            var typeDtos = _mapper.Map<IList<TaskModuleNoValidationDto>>(types);

            return typeDtos;
        }
    }
}
