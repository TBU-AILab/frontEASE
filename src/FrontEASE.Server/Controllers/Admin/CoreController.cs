using FrontEASE.Application.AppServices.Shared.Core;
using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results;
using FrontEASE.Shared.Infrastructure.Constants.Auth;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FrontEASE.Server.Controllers.Admin
{
    /// <summary>
    /// Controller for core interactions.
    /// </summary>
    [Authorize(Roles = $"{UserRoleNames.AdminRoleName},{UserRoleNames.SuperadminRoleName}")]
    public class CoreController(
        ICoreAppService coreAppService,
        IResourceHandler resourceHandler,
        IResourceAppService resourceAppService,
        AppSettings appSettings) : ApiControllerBase(resourceHandler, resourceAppService, appSettings)
    {

        /// <summary>
        /// Imports the core module package file (.zip / .py)
        /// </summary>
        /// <param name="modulesContent">Content of imported modules.</param>
        /// <returns>Python package options.</returns>
        [HttpPost($"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.Module}")]
        [ProducesResponseType(typeof(IList<ModuleImportBulkActionResultDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UnauthorizedResultDto), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> ImportCoreModule([FromBody, Required] GlobalPreferenceCoreModuleDto modulesContent)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var response = await coreAppService.ImportModules(modulesContent, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Gets the source code content of a core module
        /// </summary>
        /// <param name="name">Short name of the module.</param>
        /// <returns>Module source code content.</returns>
        [HttpGet($"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.Module}/{CoreControllerConstants.NameParam}")]
        [ProducesResponseType(typeof(CoreModuleContentDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UnauthorizedResultDto), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCoreModuleContent([FromRoute, Required] string name)
        {
            IActionResult result;
            try
            {
                var content = await coreAppService.ReadModule(name, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.OK, new CoreModuleContentDto { Content = content });
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Updates the source code content of a core module
        /// </summary>
        /// <param name="name">Short name of the module.</param>
        /// <param name="moduleContent">New module source code content.</param>
        /// <returns>No content.</returns>
        [HttpPut($"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.Module}/{CoreControllerConstants.NameParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResultDto), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateCoreModule([FromRoute, Required] string name, [FromBody, Required] CoreModuleContentDto moduleContent)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                await coreAppService.UpdateModule(name, moduleContent.Content, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Deletes the core module
        /// </summary>
        /// <param name="name">Short name of the deleted module.</param>
        /// <returns>Python package options.</returns>
        [HttpDelete($"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.Module}/{CoreControllerConstants.NameParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResultDto), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteCoreModule([FromRoute, Required] string name)
        {
            IActionResult result;
            try
            {
                await coreAppService.DeleteModule(name, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }


        /// <summary>
        /// Updates the core language models
        /// </summary>
        /// <returns>Python package options.</returns>
        [HttpPatch($"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.Models}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResultDto), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateCoreModels()
        {
            IActionResult result;
            try
            {
                await coreAppService.UpdateModels(CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Gets the available models configuration
        /// </summary>
        /// <returns>Available models JSON content.</returns>
        [HttpGet($"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.AvailableModels}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UnauthorizedResultDto), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAvailableModels()
        {
            IActionResult result;
            try
            {
                var response = await coreAppService.GetAvailableModels(CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Saves the available models configuration
        /// </summary>
        /// <param name="modelsJson">JSON string with the available models configuration.</param>
        /// <returns>No content on success.</returns>
        [HttpPut($"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.AvailableModels}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResultDto), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SaveAvailableModels([FromBody, Required] string modelsJson)
        {
            IActionResult result;
            try
            {
                await coreAppService.SaveAvailableModels(modelsJson, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.NoContent);
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
