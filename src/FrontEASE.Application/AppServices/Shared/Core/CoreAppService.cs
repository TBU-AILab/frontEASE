using AutoMapper;
using FrontEASE.Domain.Services.Core;
using FrontEASE.Domain.Services.Shared.Typelists;
using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;

namespace FrontEASE.Application.AppServices.Shared.Core
{
    public class CoreAppService(ICoreService coreService, ITypelistService typelistService, IMapper mapper) : ICoreAppService
    {
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
                    var moduleFile = mapper.Map<Domain.Entities.Shared.Files.File>(module);
                    await coreService.ImportCoreModule(moduleFile, cancellationToken);
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
                await typelistService.LoadModuleTypes(true, cancellationToken);
            }

            return resultList;
        }

        public async Task DeleteModule(string shortName, CancellationToken cancellationToken)
        {
            await coreService.DeleteCoreModule(shortName, cancellationToken);
            await typelistService.LoadModuleTypes(true, cancellationToken);
        }

        public async Task UpdateModels(CancellationToken cancellationToken) => await coreService.UpdateCoreModels(cancellationToken);
    }
}
