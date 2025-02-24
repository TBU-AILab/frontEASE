using AutoMapper;
using FoP_IMT.Client.Services.HelperServices.ErrorHandling;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FoP_IMT.Client.Services.ApiServices.Shared.Typelists
{
    public class TypelistApiService : ApiServiceBase, ITypelistApiService
    {
        public TypelistApiService(HttpClient client, IMapper mapper, IErrorHandlingService errorHandlingService) : base(client, mapper, errorHandlingService) { }

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
