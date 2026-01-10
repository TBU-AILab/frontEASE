using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Client.Services.ModelManipulationServices.Companies;
using FrontEASE.Shared.Data.DTOs.Companies;
using FrontEASE.Shared.Infrastructure.Constants.Controllers;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FrontEASE.Client.Services.ApiServices.Companies
{
    public class CompanyApiService(
        HttpClient client,
        IMapper mapper,
        IErrorHandlingService errorHandlingService,
        ICompanyManipulationService companyManipulationService
            ) : ApiServiceBase(client, mapper, errorHandlingService), ICompanyApiService
    {
        public async Task<IList<CompanyDto>> LoadCompanies()
        {
            var url = $"{CompaniesControllerConstants.BaseUrl}/{ControllerConstants.All}";
            var response = await _client.GetAsync(url);
            var expectedCodes = new List<HttpStatusCode>() { HttpStatusCode.OK, HttpStatusCode.NotFound };

            if (!expectedCodes.Contains(response.StatusCode))
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return [];
            }
            return response.StatusCode == HttpStatusCode.NotFound ? [] : (await response.Content.ReadFromJsonAsync<IList<CompanyDto>>())!;
        }

        public async Task<CompanyDto?> AddCompany(CompanyDto addCompanyDto)
        {
            companyManipulationService.ConsolidateCompanyModel(addCompanyDto);

            var response = await _client.PostAsJsonAsync(CompaniesControllerConstants.BaseUrl, addCompanyDto);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<CompanyDto>();
        }

        public async Task<CompanyDto?> UpdateCompany(CompanyDto editCompanyDto)
        {
            companyManipulationService.ConsolidateCompanyModel(editCompanyDto);

            var response = await _client.PutAsJsonAsync(CompaniesControllerConstants.BaseUrl, editCompanyDto);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<CompanyDto>();
        }

        public async Task<bool> DeleteCompany(Guid companyID)
        {
            var url = $"{CompaniesControllerConstants.BaseUrl}/{companyID}";
            var response = await _client.DeleteAsync(url);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return false;
            }
            return true;
        }
    }
}
