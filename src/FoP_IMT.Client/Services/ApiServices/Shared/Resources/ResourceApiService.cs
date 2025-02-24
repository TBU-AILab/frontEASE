using AutoMapper;
using FoP_IMT.Client.Services.HelperServices.ErrorHandling;
using FoP_IMT.Shared.Data.DTOs.Shared.Resources;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FoP_IMT.Client.Services.ApiServices.Shared.Resources
{
    public class ResourceApiService : ApiServiceBase, IResourceApiService
    {
        public ResourceApiService(HttpClient client, IMapper mapper, IErrorHandlingService errorHandlingService) : base(client, mapper, errorHandlingService) { }

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
