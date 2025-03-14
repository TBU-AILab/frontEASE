using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.DTOs.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;
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
    /// Controller for resource management.
    /// </summary>
    [AllowAnonymous]
    public class ResourcesController : ApiControllerBase
    {
        private readonly IResourceAppService _resourceAppService;

        public ResourcesController(
            IResourceAppService resourceAppService,
            IResourceHandler resourceHandler,
            AppSettings appSettings) : base(resourceHandler, resourceAppService, appSettings)
        {
            _resourceAppService = resourceAppService;
        }

        /// <summary>
        /// Gets list of text resources (translations) from API.
        /// </summary>
        /// <returns>List of resources.</returns>
        [HttpGet($"{ResourcesControllerConstants.BaseUrl}/{ControllerConstants.All}")]
        [ProducesResponseType(typeof(IList<ResourceDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetResources([FromQuery, Required] LanguageCode language)
        {
            IActionResult result;
            try
            {
                var response = await _resourceAppService.LoadAll(language);
                result = GetHttpResult(HttpStatusCode.OK, response);
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
