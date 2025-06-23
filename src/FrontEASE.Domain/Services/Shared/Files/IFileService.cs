
using FrontEASE.Shared.Data.Enums.Shared.Files;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Domain.Services.Shared.Files
{
    public interface IFileService
    {
        Task<(FileStreamResult? Stream, string? Name)?> GetArchive(Guid identifier, FileSpecification type, CancellationToken cancellationToken);
    }
}
