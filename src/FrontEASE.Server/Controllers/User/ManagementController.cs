﻿using FrontEASE.Application.AppServices.Management;
using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.Core.Packages;
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
    public class ManagementController : ApiControllerBase
    {
        private readonly IManagementAppService _managementAppService;

        public ManagementController(
            IManagementAppService managementAppService,
            IResourceHandler resourceHandler,
            IResourceAppService resourceAppService,
            AppSettings appSettings) : base(resourceHandler, resourceAppService, appSettings)
        {
            _managementAppService = managementAppService;
        }

        /// <summary>
        /// Loads current user preferences.
        /// </summary>
        /// <returns>Preferences DTO model.</returns>
        [HttpGet($"{ManagementControllerConstants.BaseUrl}")]
        [ProducesResponseType(typeof(UserPreferencesDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadPreferences()
        {
            IActionResult result;
            try
            {
                var response = await _managementAppService.Load();
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
        /// <returns>Python package options.</returns>
        [Authorize(Roles = $"{UserRoleNames.AdminRoleName},{UserRoleNames.SuperadminRoleName}")]
        [HttpGet($"{ManagementControllerConstants.BaseUrl}/{ManagementControllerConstants.Global}")]
        [ProducesResponseType(typeof(GlobalPreferenceCorePackageDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoadGlobalPreferences()
        {
            IActionResult result;
            try
            {
                var response = await _managementAppService.LoadGlobal();
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
                var updatedPreferences = await _managementAppService.Update(preferences);
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
                var updatedPreferences = await _managementAppService.UpdateGlobal(preferences);
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
