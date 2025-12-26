using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FrontEASE.Client.Services.ApiServices.Shared.Core
{
    public class CoreApiService(
        HttpClient client,
        IMapper mapper,
        IErrorHandlingService errorHandlingService) : ApiServiceBase(client, mapper, errorHandlingService), ICoreApiService
    {
        public async Task<IList<ModuleImportBulkActionResultDto>> ImportTaskCoreModules(GlobalPreferenceCoreModuleDto modules)
        {
            var url = $"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.Module}";
            var response = await _client.PostAsJsonAsync(url, modules);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return [];
            }
            return await response.Content.ReadFromJsonAsync<IList<ModuleImportBulkActionResultDto>>() ?? [];
        }


        public async Task<bool> DeleteTaskCoreModule(string moduleName)
        {
            var url = $"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.Module}/{moduleName}";
            var response = await _client.DeleteAsync(url);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateCoreModels()
        {
            var url = $"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.Models}";
            var response = await _client.PatchAsync(url, null);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return false;
            }
            return true;
        }
    }
}
