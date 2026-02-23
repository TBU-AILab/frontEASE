using FrontEASE.Application.AppServices.Management;
using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Server.Infrastructure.Swagger.Attributes;
using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.Core.Packages;
using FrontEASE.Shared.Data.DTOs.Management.Tags;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FrontEASE.Shared.Infrastructure.Constants.Auth;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FrontEASE.Server.Controllers.User
{
    /// <summary>
    /// Controller for preferences management.
    /// </summary>
    [Authorize]
    public class ManagementController(
        IManagementAppService managementAppService,
        IResourceHandler resourceHandler,
        IResourceAppService resourceAppService,
        AppSettings appSettings) : ApiControllerBase(resourceHandler, resourceAppService, appSettings)
    {
        /// <summary>
        /// Inserts a new task tag.
        /// </summary>
        /// <param name="tag">Created tag DTO.</param>
        /// <returns>Inserted tag DTO.</returns>
        [HttpPost($"{ManagementControllerConstants.BaseUrl}/{ManagementControllerConstants.Tags}")]
        [ProducesResponseType(typeof(UserPreferenceTagOptionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InsertTag([FromBody, Required] UserPreferenceTagOptionDto tag)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var response = await managementAppService.Create(tag, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.Created, response, nameof(InsertTag));
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }


        /// <summary>
        /// Deletes a task tag.
        /// </summary>
        /// <param name="tag">Deleted tag.</param>
        [HttpDelete($"{ManagementControllerConstants.BaseUrl}/{ManagementControllerConstants.Tags}/{ManagementControllerConstants.TagParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UnauthorizedResultDto), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteTag([Required, FromRoute] string tag)
        {
            IActionResult result;
            try
            {
                await managementAppService.DeleteTag(tag, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.NoContent, null);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Loads all user-defined tags.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Preferences DTO model.</returns>
        [HttpGet($"{ManagementControllerConstants.BaseUrl}/{ManagementControllerConstants.Tags}")]
        [ProducesResponseType(typeof(IList<UserPreferenceTagOptionDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadTags([ParameterSwaggerIgnore] CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await managementAppService.LoadTags(cancellationToken);
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
        /// Loads current user preferences.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Preferences DTO model.</returns>
        [HttpGet($"{ManagementControllerConstants.BaseUrl}")]
        [ProducesResponseType(typeof(UserPreferencesDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadPreferences([ParameterSwaggerIgnore] CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await managementAppService.Load(cancellationToken);
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
        /// Gets the global preferences
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Python package options.</returns>
        [Authorize(Roles = $"{UserRoleNames.AdminRoleName},{UserRoleNames.SuperadminRoleName}")]
        [HttpGet($"{ManagementControllerConstants.BaseUrl}/{ManagementControllerConstants.Global}")]
        [ProducesResponseType(typeof(GlobalPreferenceCorePackageDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoadGlobalPreferences([ParameterSwaggerIgnore] CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await managementAppService.LoadGlobal(cancellationToken);
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
        /// Updates user preferences preset.
        /// </summary>
        /// <param name="preferences">DTO with new updated user preferences.</param>
        /// <returns>User preferences DTO model.</returns>
        [HttpPut(ManagementControllerConstants.BaseUrl)]
        [ProducesResponseType(typeof(UserPreferencesDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdatePreferences([Required, FromBody] UserPreferencesDto preferences)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var updatedPreferences = await managementAppService.Update(preferences, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.OK, updatedPreferences);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Updates global preferences preset.
        /// </summary>
        /// <param name="preferences">DTO with new updated global preferences.</param>
        /// <returns>Global preferences DTO model.</returns>
        [Authorize(Roles = $"{UserRoleNames.AdminRoleName},{UserRoleNames.SuperadminRoleName}")]
        [HttpPut($"{ManagementControllerConstants.BaseUrl}/{ManagementControllerConstants.Global}")]
        [ProducesResponseType(typeof(GlobalPreferencesDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateGlobalPreferences([Required, FromBody] GlobalPreferencesDto preferences)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var updatedPreferences = await managementAppService.UpdateGlobal(preferences, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.OK, updatedPreferences);
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
