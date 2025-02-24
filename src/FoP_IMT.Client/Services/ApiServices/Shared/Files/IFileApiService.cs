using FoP_IMT.Shared.Data.Enums.Shared.Files;

namespace FoP_IMT.Client.Services.ApiServices.Shared.Files
{
    public interface IFileApiService
    {
        Task<bool> DownloadDirectory(string fileName, Guid identifier, FileSpecification type);
    }
}
