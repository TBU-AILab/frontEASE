using FrontEASE.Shared.Data.Enums.Shared.Files;

namespace FrontEASE.Client.Services.ApiServices.Shared.Files
{
    public interface IFileApiService
    {
        Task<bool> DownloadDirectory(string fileName, Guid identifier, FileSpecification type);
    }
}
