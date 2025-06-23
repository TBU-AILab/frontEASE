using AutoMapper;
using FrontEASE.Domain.Services.Core;
using FrontEASE.Domain.Services.Shared.Typelists;
using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;

namespace FrontEASE.Application.AppServices.Shared.Core
{
    public class CoreAppService : ICoreAppService
    {
        private readonly ICoreService _coreService;
        private readonly ITypelistService _typelistService;
        private readonly IMapper _mapper;

        public CoreAppService(ICoreService coreService, ITypelistService typelistService, IMapper mapper)
        {
            _coreService = coreService;
            _typelistService = typelistService;
            _mapper = mapper;
        }

        public async Task<IList<ModuleImportBulkActionResultDto>> ImportModules(GlobalPreferenceCoreModuleDto modulesContent, CancellationToken cancellationToken)
        {
            var resultList = new List<ModuleImportBulkActionResultDto>();
            foreach (var module in modulesContent.Files)
            {
                var result = new ModuleImportBulkActionResultDto()
                {
                    Name = module.Name,
                };

                try
                {
                    var moduleFile = _mapper.Map<Domain.Entities.Shared.Files.File>(module);
                    await _coreService.ImportCoreModule(moduleFile, cancellationToken);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.ExceptionMessage = ex.Message;
                }
                resultList.Add(result);
            }

            if (resultList.Any(r => r.Success))
            {
                await _typelistService.LoadModuleTypes(true, cancellationToken);
            }

            return resultList;
        }

        public async Task DeleteModule(string shortName, CancellationToken cancellationToken)
        {
            await _coreService.DeleteCoreModule(shortName, cancellationToken);
            await _typelistService.LoadModuleTypes(true, cancellationToken);
        }

        public async Task UpdateModels(CancellationToken cancellationToken) => await _coreService.UpdateCoreModels(cancellationToken);
    }
}
