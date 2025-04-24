using Blazored.LocalStorage;
using FrontEASE.Client.Services.HelperServices.Auth.Models;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Infrastructure.Constants.UI.Specific;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace FrontEASE.Client.Services.HelperServices
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IResourceHandler _resourceHandler;
        private readonly ISyncLocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(HttpClient httpClient, ISyncLocalStorageService localStorage, IResourceHandler resourceHandler)
        {
            _httpClient = httpClient;
            _resourceHandler = resourceHandler;
            _localStorage = localStorage;

            var accessToken = _localStorage.GetItem<string>("accessToken");
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            try
            {
                var response = await _httpClient.GetAsync("manage/info");
                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    var email = jsonResponse?["email"]!.ToString();
                    var username = jsonResponse?["username"]!.ToString();
                    var roles = jsonResponse?["roles"]?.AsArray();

                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, username ?? string.Empty),
                        new(ClaimTypes.Email, email ?? string.Empty)
                    };

                    if (roles is not null)
                    {
                        foreach (var role in roles)
                        {
                            var roleClaim = new Claim(ClaimTypes.Role, role!.ToString());
                            claims.Add(roleClaim);
                        }
                    }

                    var identity = new ClaimsIdentity(claims, "Token");
                    user = new ClaimsPrincipal(identity);
                    return new AuthenticationState(user);
                }
                return new AuthenticationState(user);
            }
            catch (Exception)
            {
                return new AuthenticationState(user);
            }
        }

        public void Logout()
        {
            _localStorage.RemoveItem("accessToken");
            _localStorage.RemoveItem("refreshToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<AuthAttemptResultDto> LoginAsync(string email, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("login", new { email, password });
                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    var accessToken = jsonResponse?["accessToken"]?.ToString();
                    var refreshToken = jsonResponse?["refreshToken"]?.ToString();

                    _localStorage.SetItem("accessToken", accessToken);
                    _localStorage.SetItem("refreshToken", refreshToken);

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                    return new AuthAttemptResultDto { Success = true };
                }
                else
                {
                    var errorResource = _resourceHandler.GetResource($"{UIConstants.Base}.{UIConstants.Generic}.{UIConstants.Error}.{UIActionConstants.Login}");
                    return new AuthAttemptResultDto { Success = false, Errors = [errorResource] };
                }
            }
            catch (Exception ex)
            {
                return new AuthAttemptResultDto { Success = false, Errors = [ex.Message] };
            }
        }
    }
}
