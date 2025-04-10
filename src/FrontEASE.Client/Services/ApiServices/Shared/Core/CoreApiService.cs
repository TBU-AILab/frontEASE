using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FrontEASE.Client.Services.ApiServices.Shared.Core
{
    public class CoreApiService : ApiServiceBase, ICoreApiService
    {
        public CoreApiService(
            HttpClient client,
            IMapper mapper,
            IErrorHandlingService errorHandlingService) : base(client, mapper, errorHandlingService)
        { }

        public async Task<IList<ModuleImportBulkActionResultDto>> ImportTaskModules(GlobalPreferenceCoreModuleDto modules)
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
    }
}
