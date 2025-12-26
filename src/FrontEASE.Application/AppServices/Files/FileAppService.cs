using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Services.Shared.Files;
using FrontEASE.Shared.Data.Enums.Shared.Files;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Application.AppServices.Files
{
    public class FileAppService(IFileService fileService) : IFileAppService
    {
        public async Task<FileStreamResult> GetDirectory(Guid identifier, FileSpecification type, CancellationToken cancellationToken)
        {
            var archive = await fileService.GetArchive(identifier, type, cancellationToken) ?? throw new NotFoundException();
            archive.Stream!.FileDownloadName = archive.Name;

            return archive.Stream;
        }
    }
}
