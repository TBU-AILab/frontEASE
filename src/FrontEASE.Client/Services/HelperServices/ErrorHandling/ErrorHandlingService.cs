using Blazored.Toast.Services;
using FrontEASE.Client.Services.HelperServices.UI.Manage;
using FrontEASE.Client.Shared.Styling.Defaults;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FrontEASE.Shared.Data.Enums.Users.Preferences.GeneralOptions;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Infrastructure.Constants.UI.Specific;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace FrontEASE.Client.Services.HelperServices.ErrorHandling
{
    public class ErrorHandlingService : IErrorHandlingService
    {
        private readonly IToastService _toastService;
        private readonly IResourceHandler _resourceHandler;
        private readonly IUIManager _uiManager;
        private readonly NavigationManager _navManager;

        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public ErrorHandlingService(
            IToastService toastService,
            IResourceHandler resourceHandler,
            IUIManager uiManager,
            NavigationManager navManager,
            AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            _resourceHandler = resourceHandler;
            _toastService = toastService;
            _uiManager = uiManager;
            _navManager = navManager;
        }

        public async Task HandleErrorResponse(HttpResponseMessage httpResponse)
        {
            var messageLevel = ToastLevel.Error;
            var uiMessageBody = string.Empty;
            var uiMessageTitle = string.Empty;

            var msgBody = _resourceHandler.GetResource($"{UIConstants.Base}.{UIConstants.Error}.{httpResponse.StatusCode}.{UIStateConstants.Text}");
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    {
                        var responseString = await httpResponse.Content.ReadAsStringAsync();
                        var responseMessage = JsonSerializer.Deserialize<BadRequestResultDto>(responseString);

                        var problemsList = new List<Tuple<string, string>>();

                        foreach (var error in responseMessage!.Errors)
                        {
                            foreach (var problem in error.Problems)
                            {
                                problemsList.Add(new Tuple<string, string>(error.Key, problem));
                            }
                        }
                        uiMessageBody = $"{msgBody}: {string.Join(", ", problemsList.Select(x => x.Item2))}";
                        uiMessageTitle = _resourceHandler.GetResource(responseMessage?.Message ?? string.Empty);
                    }
                    break;
                case HttpStatusCode.UnprocessableContent:
                    {
                        var responseString = await httpResponse.Content.ReadAsStringAsync();
                        var responseMessage = JsonSerializer.Deserialize<UnprocessableResultDto>(responseString);

                        uiMessageBody = $"{msgBody}: {string.Join(", ", responseMessage!.Errors)}";
                        uiMessageTitle = _resourceHandler.GetResource(responseMessage?.Message ?? string.Empty);
                        messageLevel = ToastLevel.Warning;
                    }
                    break;
                case HttpStatusCode.NotFound:
                    {
                        var responseString = await httpResponse.Content.ReadAsStringAsync();
                        var responseMessage = JsonSerializer.Deserialize<NotFoundResultDto>(responseString);

                        uiMessageTitle = _resourceHandler.GetResource(responseMessage?.Message ?? string.Empty);
                        uiMessageBody = msgBody;
                    }
                    break;
                case HttpStatusCode.Unauthorized:
                    {
                        var state = await _authStateProvider.GetAuthenticationStateAsync();
                        if (state.User.Identity?.IsAuthenticated != true)
                        {
                            _authStateProvider.Logout();

                            var loginRoute = _resourceHandler.GetResource($"{UIConstants.Base}.{UIConstants.Specific}.{UIElementConstants.Route}.{UIRouteConstants.AccountRoute}.{UIRouteConstants.LoginRoute}");
                            _uiManager.ChangeTheme(ThemeDefaults.GetThemeDefaults(ColorScheme.LIGHT));
                            _navManager.NavigateTo(loginRoute);
                        }
                        else
                        {
                            var responseMessage = await httpResponse.Content.ReadFromJsonAsync<UnauthorizedResultDto>();
                            uiMessageTitle = _resourceHandler.GetResource(responseMessage?.Message ?? string.Empty);
                            uiMessageBody = msgBody;
                            messageLevel = ToastLevel.Warning;
                        }
                    }
                    break;
                case HttpStatusCode.InternalServerError:
                    {
                        var responseMessage = await httpResponse.Content.ReadFromJsonAsync<InternalErrorResultDto>();
                        uiMessageTitle = _resourceHandler.GetResource(responseMessage?.Message ?? string.Empty);
                        uiMessageBody = msgBody;
                    }
                    break;
                default:
                    break;
            }

            await VisualizeException(uiMessageTitle ?? "N/A", uiMessageBody, messageLevel);
        }

        private async Task VisualizeException(string title, string body, ToastLevel level)
        {
            await Console.Out.WriteLineAsync(title);
            await Console.Out.WriteLineAsync(body);

            var content = string.IsNullOrWhiteSpace(title) ? $"{body}" : $"{title.ToUpper()} : {body}";
            switch (level)
            {
                case ToastLevel.Error:
                    _toastService.ShowError(content);
                    break;
                case ToastLevel.Warning:
                    _toastService.ShowWarning(content);
                    break;
                default:
                    _toastService.ShowInfo(content);
                    break;
            }
        }
    }
}
