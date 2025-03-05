using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Services.Shared.Files;
using FrontEASE.Shared.Data.Enums.Shared.Files;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Application.AppServices.Files
{
    public class FileAppService : IFileAppService
    {
        private readonly IFileService _fileService;
        private readonly AppSettings _appSettings;

        public FileAppService(IFileService fileService, AppSettings appSettings)
        {
            _appSettings = appSettings;
            _fileService = fileService;
        }

        public async Task<FileStreamResult> GetDirectory(Guid identifier, FileSpecification type)
        {
            var archive = await _fileService.GetArchive(identifier, type) ?? throw new NotFoundException();
            archive.Stream!.FileDownloadName = archive.Name;

            return archive.Stream;
        }
    }
}
