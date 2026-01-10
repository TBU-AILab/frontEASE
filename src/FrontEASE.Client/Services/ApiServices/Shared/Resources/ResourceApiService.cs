using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Shared.Data.DTOs.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;
using FrontEASE.Shared.Infrastructure.Constants.Controllers;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FrontEASE.Client.Services.ApiServices.Shared.Resources
{
    public class ResourceApiService(HttpClient client, IMapper mapper, IErrorHandlingService errorHandlingService) : ApiServiceBase(client, mapper, errorHandlingService), IResourceApiService
    {
        public async Task<IList<ResourceDto>> LoadResources(LanguageCode language)
        {
            var url = string.Format($"{ResourcesControllerConstants.BaseUrl}/{ControllerConstants.All}?{ResourcesControllerConstants.LanguageParam}=" + "{0}", language);
            var response = await _client.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return [];
            }
            return (await response.Content.ReadFromJsonAsync<IList<ResourceDto>>())!;
        }
    }
}
