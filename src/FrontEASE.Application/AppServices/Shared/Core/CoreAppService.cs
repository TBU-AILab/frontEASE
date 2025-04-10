using AutoMapper;
using FrontEASE.Domain.Services.Core;
using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;

namespace FrontEASE.Application.AppServices.Shared.Core
{
    public class CoreAppService : ICoreAppService
    {
        private readonly ICoreService _coreService;
        private readonly IMapper _mapper;

        public CoreAppService(ICoreService coreService, IMapper mapper)
        {
            _coreService = coreService;
            _mapper = mapper;
        }

        public async Task<IList<ModuleImportBulkActionResultDto>> ImportModules(GlobalPreferenceCoreModuleDto modulesContent)
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
                    await _coreService.ImportCoreModule(moduleFile);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.ExceptionMessage = ex.Message;
                }
                resultList.Add(result);
            }
            return resultList;
        }
    }
}
