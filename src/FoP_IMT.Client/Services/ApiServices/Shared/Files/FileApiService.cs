using AutoMapper;
using FoP_IMT.Client.Services.HelperServices.ErrorHandling;
using FoP_IMT.Shared.Data.Enums.Shared.Files;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific;
using Microsoft.JSInterop;
using System.Net;

namespace FoP_IMT.Client.Services.ApiServices.Shared.Files
{
    public class FileApiService : ApiServiceBase, IFileApiService
    {
        private readonly IJSRuntime _jsRuntime;

        public FileApiService(HttpClient client, IMapper mapper, IErrorHandlingService errorHandlingService, IJSRuntime jsRuntime) : base(client, mapper, errorHandlingService)
        {
            _jsRuntime = jsRuntime;
        }

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
            await _jsRuntime.InvokeVoidAsync("downloadFileFromStream", archiveName, streamRef);

            return true;
        }
    }
}
