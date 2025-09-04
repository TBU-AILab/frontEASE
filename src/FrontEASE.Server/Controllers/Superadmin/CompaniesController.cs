using FrontEASE.Application.AppServices.Companies;
using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FrontEASE.Shared.Infrastructure.Constants.Auth;
using FrontEASE.Shared.Infrastructure.Constants.Controllers;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FrontEASE.Server.Controllers.Superadmin
{
    /// <summary>
    /// Controller for company management.
    /// </summary>
    [Authorize]
    public class CompaniesController : ApiControllerBase
    {
        private readonly ICompanyAppService _companyAppService;
        public CompaniesController(
            ICompanyAppService companyAppService,
            IResourceHandler resourceHandler,
            IResourceAppService resourceAppService,
            AppSettings appSettings) : base(resourceHandler, resourceAppService, appSettings)
        {
            _companyAppService = companyAppService;
        }

        /// <summary>
        /// Gets list of companies.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List of companies.</returns>
        [HttpGet($"{CompaniesControllerConstants.BaseUrl}/{ControllerConstants.All}")]
        [ProducesResponseType(typeof(IList<CompanyDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCompanies(CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await _companyAppService.LoadAll(cancellationToken);
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
        /// Loads detailed info about individual company.
        /// </summary>
        /// <param name="id">Company identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Company DTO model.</returns>
        [HttpGet($"{CompaniesControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(CompanyDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadCompany([Required, FromRoute] Guid id, CancellationToken cancellationToken)
        {
            IActionResult result;
            try
            {
                var response = await _companyAppService.Load(id, cancellationToken);
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
        /// Creates new company item.
        /// </summary>
        /// <param name="company">DTO with new company info.</param>
        /// <returns>Company DTO model.</returns>
        [Authorize(Roles = $"{UserRoleNames.SuperadminRoleName}")]
        [HttpPost(CompaniesControllerConstants.BaseUrl)]
        [ProducesResponseType(typeof(CompanyDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCompany([Required, FromBody] CompanyDto company)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var createdCompany = await _companyAppService.Create(company, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.Created, createdCompany, nameof(CreateCompany));
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Updates info about company.
        /// </summary>
        /// <param name="company">DTO with modified company info.</param>
        /// <returns>Company DTO model.</returns>
        [Authorize(Roles = $"{UserRoleNames.SuperadminRoleName}")]
        [HttpPut(CompaniesControllerConstants.BaseUrl)]
        [ProducesResponseType(typeof(CompanyDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCompany([Required, FromBody] CompanyDto company)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var updatedCompany = await _companyAppService.Update(company, CancellationToken.None);
                result = GetHttpResult(HttpStatusCode.OK, updatedCompany);
            }
            catch (Exception ex)
            {
                var response = ProcessApiException(ex);
                result = GetHttpResult(response!.StatusCode, response);
            }
            return result;
        }

        /// <summary>
        /// Deletes a company.
        /// </summary>
        /// <param name="id">Deleted company identifier.</param>
        [Authorize(Roles = $"{UserRoleNames.SuperadminRoleName}")]
        [HttpDelete($"{CompaniesControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCompany([Required, FromRoute] Guid id)
        {
            IActionResult result;
            try
            {
                await _companyAppService.Delete(id, CancellationToken.None);
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
