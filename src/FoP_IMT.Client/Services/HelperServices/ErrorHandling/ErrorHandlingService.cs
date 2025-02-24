using Blazored.Toast.Services;
using FoP_IMT.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FoP_IMT.Shared.Infrastructure.Constants.UI.Generic;
using FoP_IMT.Shared.Services.Resources;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace FoP_IMT.Client.Services.HelperServices.ErrorHandling
{
    public class ErrorHandlingService : IErrorHandlingService
    {
        private readonly IToastService _toastService;
        private readonly IResourceHandler _resourceHandler;

        public ErrorHandlingService(IToastService toastService, IResourceHandler resourceHandler)
        {
            _resourceHandler = resourceHandler;
            _toastService = toastService;
        }

        public async Task HandleErrorResponse(HttpResponseMessage httpResponse)
        {
            var uiMessageBody = string.Empty;
            var uiMessageTitle = string.Empty;

            var msgBody = _resourceHandler.GetResource($"{UIConstants.Base}.{UIConstants.Error}.{httpResponse.StatusCode}.{UIStateConstants.Text}");
            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    {
                        var responseString = await httpResponse.Content.ReadAsStringAsync();
                        var responseMessage = JsonConvert.DeserializeObject<BadRequestResultDto>(responseString);

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
                case HttpStatusCode.NotFound:
                    {
                        var responseString = await httpResponse.Content.ReadAsStringAsync();
                        var responseMessage = JsonConvert.DeserializeObject<NotFoundResultDto>(responseString);

                        uiMessageTitle = _resourceHandler.GetResource(responseMessage?.Message ?? string.Empty);
                        uiMessageBody = msgBody;
                    }
                    break;
                case HttpStatusCode.Unauthorized:
                    {
                        var responseMessage = await httpResponse.Content.ReadFromJsonAsync<UnauthorizedResultDto>();
                        uiMessageTitle = _resourceHandler.GetResource(responseMessage?.Message ?? string.Empty);
                        uiMessageBody = msgBody;
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

            await VisualizeException(uiMessageTitle ?? "N/A", uiMessageBody);
        }

        private async Task VisualizeException(string title, string body)
        {
            await Console.Out.WriteLineAsync(title);
            await Console.Out.WriteLineAsync(body);

            _toastService.ShowError(string.IsNullOrWhiteSpace(title) ? $"{body}" : $"{title.ToUpper()} : {body}");
        }
    }
}
