namespace FoP_IMT.Client.Services.HelperServices.ErrorHandling
{
    public interface IErrorHandlingService
    {
        Task HandleErrorResponse(HttpResponseMessage httpResponse);
    }
}
