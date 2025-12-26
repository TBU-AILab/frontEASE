using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Shared.Data.Enums.Shared.Files;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using Microsoft.JSInterop;
using System.Net;

namespace FrontEASE.Client.Services.ApiServices.Shared.Files
{
    public class FileApiService(HttpClient client, IMapper mapper, IErrorHandlingService errorHandlingService, IJSRuntime jsRuntime) : ApiServiceBase(client, mapper, errorHandlingService), IFileApiService
    {
        public async Task<bool> DownloadDirectory(string fileName, Guid identifier, FileSpecification type)
        {
            var url = $"{FilesControllerConstants.BaseUrl}/{FilesControllerConstants.Directory}/{(int)type}/{identifier}";
            var response = await _client.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return false;
            }

            var contentStream = await response.Content.ReadAsStreamAsync();
            using var streamRef = new DotNetStreamReference(contentStream);

            var archiveName = $"{fileName}.zip";
            await jsRuntime.InvokeVoidAsync("downloadFileFromStream", archiveName, streamRef);

            return true;
        }
    }
}
