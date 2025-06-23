using FrontEASE.Shared.Data.Enums.Shared.Files;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Application.AppServices.Files
{
    public interface IFileAppService
    {
        Task<FileStreamResult> GetDirectory(Guid identifier, FileSpecification type, CancellationToken cancellationToken);
    }
}
