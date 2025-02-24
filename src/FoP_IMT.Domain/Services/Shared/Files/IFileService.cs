
using FoP_IMT.Shared.Data.Enums.Shared.Files;
using Microsoft.AspNetCore.Mvc;

namespace FoP_IMT.Domain.Services.Shared.Files
{
    public interface IFileService
    {
        Task<(FileStreamResult? Stream, string? Name)?> GetArchive(Guid identifier, FileSpecification type);
    }
}
