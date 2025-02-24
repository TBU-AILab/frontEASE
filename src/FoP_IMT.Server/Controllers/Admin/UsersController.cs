using FoP_IMT.Application.AppServices.Shared.Resources;
using FoP_IMT.Application.AppServices.Users;
using FoP_IMT.Domain.Infrastructure.Exceptions;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FoP_IMT.Shared.Data.DTOs.Shared.Users;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific;
using FoP_IMT.Shared.Services.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FoP_IMT.Server.Controllers.Superadmin
{
    /// <summary>
    /// Controller for user management.
    /// </summary>
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
        /// Gets list of users visible to logged admin.
        /// </summary>
        /// <returns>List of users.</returns>
        [HttpGet($"{UsersControllerConstants.BaseUrl}/{ControllerConstants.All}")]
        [ProducesResponseType(typeof(IList<ApplicationUserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsers()
        {
            IActionResult result;
            try
            {
                var response = await _userAppService.LoadAll();
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
        [HttpPost($"{UsersControllerConstants.BaseUrl}")]
        [ProducesResponseType(typeof(ApplicationUserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InsertUser([FromBody, Required] ApplicationUserDto user)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var response = await _userAppService.Create(user);
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
                var updatedUser = await _userAppService.Update(user);
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
        [HttpDelete($"{UsersControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUser([Required, FromRoute] Guid id)
        {
            IActionResult result;
            try
            {
                await _userAppService.Delete(id);
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
