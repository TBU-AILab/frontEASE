using FoP_IMT.Application.AppServices.Shared.Resources;
using FoP_IMT.Application.AppServices.Shared.Typelists;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific;
using FoP_IMT.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FoP_IMT.Server.Controllers.Anonymous
{
    /// <summary>
    /// Controller for typelists retrieval.
    /// </summary>
    [AllowAnonymous]
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
        /// <returns>Module dynamic options.</returns>
        [HttpGet($"{TypelistsControllerConstants.BaseUrl}/{TypelistsControllerConstants.ModuleOptions}")]
        [ProducesResponseType(typeof(IList<TaskModuleNoValidationDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetModuleTypes()
        {
            IActionResult result;
            try
            {
                var response = await _typelistAppService.LoadModuleTypes();
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
