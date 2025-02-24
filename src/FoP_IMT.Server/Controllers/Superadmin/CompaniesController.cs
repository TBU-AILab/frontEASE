using FoP_IMT.Application.AppServices.Companies;
using FoP_IMT.Application.AppServices.Shared.Resources;
using FoP_IMT.Domain.Infrastructure.Exceptions;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Shared.Data.DTOs.Companies;
using FoP_IMT.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific;
using FoP_IMT.Shared.Services.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FoP_IMT.Server.Controllers.Superadmin
{
    /// <summary>
    /// Controller for company management.
    /// </summary>
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
        /// <returns>List of companies.</returns>
        [HttpGet($"{CompaniesControllerConstants.BaseUrl}/{ControllerConstants.All}")]
        [ProducesResponseType(typeof(IList<CompanyDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCompanies()
        {
            IActionResult result;
            try
            {
                var response = await _companyAppService.LoadAll();
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
        /// <returns>Company DTO model.</returns>
        [HttpGet($"{CompaniesControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType(typeof(CompanyDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadCompany([Required, FromRoute] Guid id)
        {
            IActionResult result;
            try
            {
                var response = await _companyAppService.Load(id);
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
        [HttpPost(CompaniesControllerConstants.BaseUrl)]
        [ProducesResponseType(typeof(CompanyDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestResultDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCompany([Required, FromBody] CompanyDto company)
        {
            IActionResult result;
            try
            {
                ValidateModel();
                var createdCompany = await _companyAppService.Create(company);
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
                var updatedCompany = await _companyAppService.Update(company);
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
        [HttpDelete($"{CompaniesControllerConstants.BaseUrl}/{ControllerConstants.IdParam}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(NotFoundResultDto), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCompany([Required, FromRoute] Guid id)
        {
            IActionResult result;
            try
            {
                await _companyAppService.Delete(id);
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
