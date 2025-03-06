using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;
using FrontEASE.Client.Services.ModelManipulationServices.User;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Infrastructure.Constants.Controllers;
using FrontEASE.Shared.Infrastructure.Constants.Controllers.Specific;
using System.Net;
using System.Net.Http.Json;

namespace FrontEASE.Client.Services.ApiServices.Shared.Users
{
    public class UserApiService : ApiServiceBase, IUserApiService
    {
        private readonly IUserManipulationService _userManipulationService;

        public UserApiService(
            HttpClient client,
            IMapper mapper,
            IErrorHandlingService errorHandlingService,
            IUserManipulationService userManipulationService) :
            base(client, mapper, errorHandlingService)
        {
            _userManipulationService = userManipulationService;
        }

        public async Task<ApplicationUserDto> LoadUser()
        {
            var url = $"{UsersControllerConstants.BaseUrl}";
            var response = await _client.GetAsync(url);
            var expectedCodes = new List<HttpStatusCode>() { HttpStatusCode.OK, HttpStatusCode.NotFound };

            if (!expectedCodes.Contains(response.StatusCode))
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null!;
            }

            return response.StatusCode == HttpStatusCode.NotFound ? null! : (await response.Content.ReadFromJsonAsync<ApplicationUserDto>())!;
        }

        public async Task<IList<ApplicationUserDto>> LoadUsers()
        {
            var url = $"{UsersControllerConstants.BaseUrl}/{ControllerConstants.All}";
            var response = await _client.GetAsync(url);
            var expectedCodes = new List<HttpStatusCode>() { HttpStatusCode.OK, HttpStatusCode.NotFound };

            if (!expectedCodes.Contains(response.StatusCode))
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return [];
            }

            return response.StatusCode == HttpStatusCode.NotFound ? [] : (await response.Content.ReadFromJsonAsync<IList<ApplicationUserDto>>())!;
        }

        public async Task<ApplicationUserDto?> AddUser(ApplicationUserDto addUserDto)
        {
            _userManipulationService.ConsolidateUserModel(addUserDto);

            var response = await _client.PostAsJsonAsync(UsersControllerConstants.BaseUrl, addUserDto);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<ApplicationUserDto>();
        }

        public async Task<ApplicationUserDto?> UpdateUser(ApplicationUserDto editUserDto)
        {
            _userManipulationService.ConsolidateUserModel(editUserDto);

            var response = await _client.PutAsJsonAsync(UsersControllerConstants.BaseUrl, editUserDto);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await _errorHandlingService.HandleErrorResponse(response);
                return null;
            }
            return await response.Content.ReadFromJsonAsync<ApplicationUserDto>();
        }

        public async Task<bool> DeleteUser(Guid userID)
        {
            var url = $"{UsersControllerConstants.BaseUrl}/{userID}";
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
