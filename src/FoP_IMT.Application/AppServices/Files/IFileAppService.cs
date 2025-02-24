using FoP_IMT.Shared.Data.Enums.Shared.Files;
using Microsoft.AspNetCore.Mvc;

namespace FoP_IMT.Application.AppServices.Files
{
    public interface IFileAppService
    {
        Task<FileStreamResult> GetDirectory(Guid identifier, FileSpecification type);
    }
}
