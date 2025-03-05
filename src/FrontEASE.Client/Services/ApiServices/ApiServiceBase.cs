using AutoMapper;
using FrontEASE.Client.Services.HelperServices.ErrorHandling;

namespace FrontEASE.Client.Services.ApiServices
{
    public abstract class ApiServiceBase
    {
        public readonly HttpClient _client;
        public readonly IMapper _mapper;

        protected readonly IErrorHandlingService _errorHandlingService;

        public ApiServiceBase(HttpClient client, IMapper mapper, IErrorHandlingService errorHandlingService)
        {
            _client = client;
            _mapper = mapper;
            _errorHandlingService = errorHandlingService;
        }
    }
}
