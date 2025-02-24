using FoP_IMT.Application.AppServices.Files;
using FoP_IMT.Application.AppServices.Shared.Resources;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Shared.Data.Enums.Shared.Files;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific;
using FoP_IMT.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FoP_IMT.Server.Controllers.Anonymous
{
    /// <summary>
    /// Controller for file retrieval.
    /// </summary>
    [AllowAnonymous]
    public class FilesController : ApiControllerBase
    {
        private readonly IFileAppService _fileAppService;

        public FilesController(
            IFileAppService fileAppService,
            IResourceHandler resourceHandler,
            IResourceAppService resourceAppService,
            AppSettings appSettings) : base(resourceHandler, resourceAppService, appSettings)
        {
            _fileAppService = fileAppService;
        }

        /// <summary>
        /// Gets the directory as a .zip archive.
        /// </summary>
        /// <returns>.ZIP archive of directory.</returns>
        [HttpGet($"{FilesControllerConstants.BaseUrl}/{FilesControllerConstants.Directory}/{FilesControllerConstants.TypeParam}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDirectory([FromRoute, Required] Guid id, [FromRoute, Required] FileSpecification type)
        {
            IActionResult result;
            try
            {
                var response = await _fileAppService.GetDirectory(id, type);
                return response;
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }
    }
}
