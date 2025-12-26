using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;

namespace FrontEASE.Client.Services.ApiServices
{
    public abstract class ApiServiceBase(HttpClient client, IMapper mapper, IErrorHandlingService errorHandlingService)
    {
        public readonly HttpClient _client = client;
        public readonly IMapper _mapper = mapper;

        protected readonly IErrorHandlingService _errorHandlingService = errorHandlingService;
    }
}
