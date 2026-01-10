using FrontEASE.Application.AppServices.Files;
using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.Enums.Shared.Files;
using FrontEASE.Shared.Infrastructure.Constants.Controllers;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FrontEASE.Server.Controllers.Anonymous
{
    /// <summary>
    /// Controller for file retrieval.
    /// </summary>
    [Authorize]
    public class FilesController(
        IFileAppService fileAppService,
        IResourceHandler resourceHandler,
        IResourceAppService resourceAppService,
        AppSettings appSettings) : ApiControllerBase(resourceHandler, resourceAppService, appSettings)
    {

        /// <summary>
        /// Gets the directory as a .zip archive.
        /// </summary>
        /// <param name="id">Directory contextual ID.</param>
        /// <param name="type">Type of file to be downloaded.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>.ZIP archive of directory.</returns>
        [HttpGet($"{FilesControllerConstants.BaseUrl}/{FilesControllerConstants.Directory}/{FilesControllerConstants.TypeParam}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDirectory([FromRoute, Required] Guid id, [FromRoute, Required] FileSpecification type, CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await fileAppService.GetDirectory(id, type, cancellationToken);
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
