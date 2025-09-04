using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Application.AppServices.Shared.Typelists;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FrontEASE.Server.Controllers.Anonymous
{
    /// <summary>
    /// Controller for typelists retrieval.
    /// </summary>
    [Authorize]
    public class TypelistsController : ApiControllerBase
    {
        private readonly ITypelistAppService _typelistAppService;

        public TypelistsController(
            ITypelistAppService typelistAppService,
            IResourceHandler resourceHandler,
            IResourceAppService resourceAppService,
            AppSettings appSettings) : base(resourceHandler, resourceAppService, appSettings)
        {
            _typelistAppService = typelistAppService;
        }

        /// <summary>
        /// Gets the module types typelist.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Module dynamic options.</returns>
        [HttpGet($"{TypelistsControllerConstants.BaseUrl}/{TypelistsControllerConstants.ModuleOptions}")]
        [ProducesResponseType(typeof(IList<TaskModuleNoValidationDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetModuleTypes(CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await _typelistAppService.LoadModuleTypes(cancellationToken);
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
