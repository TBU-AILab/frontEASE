﻿using AutoMapper;
using FoP_IMT.Client.Services.HelperServices.ErrorHandling;
using FoP_IMT.Client.Services.ModelManipulationServices.Management;
using FoP_IMT.Shared.Data.DTOs.Management;
using FoP_IMT.Shared.Data.DTOs.Shared.Users;
using FoP_IMT.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FoP_IMT.Client.Services.ApiServices.Management
{
    public class ManagementApiService : ApiServiceBase, IManagementApiService
    {
        private readonly IManagementManipulationService _managementManipulationService;

        public ManagementApiService(
            HttpClient client,
            IMapper mapper,
            IErrorHandlingService errorHandlingService,
            IManagementManipulationService managementManipulationService) : 
            base(client, mapper, errorHandlingService)
        {
            _managementManipulationService = managementManipulationService;
        }

        public async Task<UserPreferencesDto?> LoadPreferences()
        {
            var url = string.Format($"{ManagementControllerConstants.BaseUrl}");
            var response = await _client.GetAsync(url);
            var expectedCodes = new List<HttpStatusCode>() { HttpStatusCode.OK, HttpStatusCode.NotFound };

            if (!expectedCodes.Contains(response.StatusCode))
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return response.StatusCode == HttpStatusCode.NotFound ? null : (await response.Content.ReadFromJsonAsync<UserPreferencesDto>())!;
        }

        public async Task<UserPreferencesDto?> UpdatePreferences(UserPreferencesDto editedPreferencesDto)
        {
            _managementManipulationService.SetItemPriorities(editedPreferencesDto);

            var response = await _client.PutAsJsonAsync(ManagementControllerConstants.BaseUrl, editedPreferencesDto);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<UserPreferencesDto>();
        }
    }
}
