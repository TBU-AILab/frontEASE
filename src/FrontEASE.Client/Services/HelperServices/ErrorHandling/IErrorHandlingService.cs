namespace FrontEASE.Client.Services.HelperServices.ErrorHandling
{
    public interface IErrorHandlingService
    {
        Task HandleErrorResponse(HttpResponseMessage httpResponse);
    }
}
