using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FrontEASE.Client.Services.ApiServices.Shared.Typelists
{
    public class TypelistApiService(HttpClient client, IMapper mapper, IErrorHandlingService errorHandlingService) : ApiServiceBase(client, mapper, errorHandlingService), ITypelistApiService
    {
        private async Task<IList<T>> GetTaskTypelists<T>(string url)
        {
            var response = await _client.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return [];
            }
            return (await response.Content.ReadFromJsonAsync<IList<T>>())!;
        }

        public async Task<IList<TaskModuleNoValidationDto>> LoadTaskModuleOptions()
        {
            var url = string.Format($"{TypelistsControllerConstants.BaseUrl}/{TypelistsControllerConstants.ModuleOptions}");
            return await GetTaskTypelists<TaskModuleNoValidationDto>(url);
        }
    }
}
