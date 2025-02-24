using AutoMapper;
using FoP_IMT.Domain.Services.Shared.Typelists;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FoP_IMT.Application.AppServices.Shared.Typelists
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
            var types = await _typelistService.LoadModuleTypes();
            var typeDtos = _mapper.Map<IList<TaskModuleNoValidationDto>>(types);

            return typeDtos;
        }
    }
}
