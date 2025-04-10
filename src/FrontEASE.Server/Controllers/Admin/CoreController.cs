﻿using FrontEASE.Application.AppServices.Shared.Core;
using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.DTOs.Management.Core.Modules;
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
    public class CoreController : ApiControllerBase
    {
        private readonly ICoreAppService _coreAppService;

        public CoreController(
            ICoreAppService coreAppService,
            IResourceHandler resourceHandler,
            IResourceAppService resourceAppService,
            AppSettings appSettings) : base(resourceHandler, resourceAppService, appSettings)
        {
            _coreAppService = coreAppService;
        }

        /// <summary>
        /// Imports the package file (.zip / .py)
        /// </summary>
        /// <param name="modulesContent">Content of imported modules.</param>
        /// <returns>Python package options.</returns>
        [HttpPost($"{CoreControllerConstants.BaseUrl}/{CoreControllerConstants.Module}")]
        [ProducesResponseType(typeof(IList<ModuleImportBulkActionResultDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ImportCoreModule([FromBody, Required] GlobalPreferenceCoreModuleDto modulesContent)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var response = await _coreAppService.ImportModules(modulesContent);
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
