using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Services.Shared.Files;
using FrontEASE.Shared.Data.Enums.Shared.Files;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Application.AppServices.Files
{
    public class FileAppService : IFileAppService
    {
        private readonly IFileService _fileService;

        public FileAppService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<FileStreamResult> GetDirectory(Guid identifier, FileSpecification type, CancellationToken cancellationToken)
        {
            var archive = await _fileService.GetArchive(identifier, type, cancellationToken) ?? throw new NotFoundException();
            archive.Stream!.FileDownloadName = archive.Name;

            return archive.Stream;
        }
    }
}
