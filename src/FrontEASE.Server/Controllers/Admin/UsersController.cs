using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Application.AppServices.Users;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Infrastructure.Constants.Auth;
using FrontEASE.Shared.Infrastructure.Constants.Controllers;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FrontEASE.Server.Controllers.Admin
{
    /// <summary>
    /// Controller for user management.
    /// </summary>
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(
            IUserAppService userAppService,
            IResourceHandler resourceHandler,
            IResourceAppService resourceAppService,
            AppSettings appSettings) : base(resourceHandler, resourceAppService, appSettings)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet($"{UsersControllerConstants.BaseUrl}")]
        [ProducesResponseType(typeof(ApplicationUserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await _userAppService.Load(cancellationToken);
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
        /// Gets list of users visible to logged admin.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List of users.</returns>
        [HttpGet($"{UsersControllerConstants.BaseUrl}/{ControllerConstants.All}")]
        [ProducesResponseType(typeof(IList<ApplicationUserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await _userAppService.LoadAll(cancellationToken);
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
        /// Inserts a new application user.
        /// </summary>
        /// <param name="user">Created user DTO.</param>
        /// <returns>Inserted user DTO.</returns>
        [Authorize(Roles = $"{UserRoleNames.AdminRoleName},{UserRoleNames.SuperadminRoleName}")]
        [HttpPost($"{UsersControllerConstants.BaseUrl}")]
        [ProducesResponseType(typeof(ApplicationUserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InsertUser([FromBody, Required] ApplicationUserDto user)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var response = await _userAppService.Create(user, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.Created, response, nameof(InsertUser));
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Updates info about application user.
        /// </summary>
        /// <param name="user">DTO with modified user info.</param>
        /// <returns>Edited user DTO.</returns>
        [Authorize(Roles = $"{UserRoleNames.AdminRoleName},{UserRoleNames.SuperadminRoleName}")]
        [HttpPut(UsersControllerConstants.BaseUrl)]
        [ProducesResponseType(typeof(ApplicationUserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUser([Required, FromBody] ApplicationUserDto user)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var updatedUser = await _userAppService.Update(user, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.OK, updatedUser);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }


        /// <summary>
        /// Deletes an application user.
        /// </summary>
        /// <param name="id">Deleted user identifier.</param>
        [Authorize(Roles = $"{UserRoleNames.AdminRoleName},{UserRoleNames.SuperadminRoleName}")]
        [HttpDelete($"{UsersControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUser([Required, FromRoute] Guid id)
        {
            IActionResult result;
            try
            {
                await _userAppService.Delete(id, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.NoContent, null);
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
