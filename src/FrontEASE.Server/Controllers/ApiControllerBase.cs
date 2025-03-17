using FrontEASE.Application.AppServices.Shared.Resources;
using FrontEASE.Domain.Infrastructure.Exceptions;
using FrontEASE.Domain.Infrastructure.Exceptions.Enums;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Server.Infrastructure.Utils;
using FrontEASE.Server.Infrastructure.Utils.Enums;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Services.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FrontEASE.Server.Controllers
{
    /// <summary>
    /// Base controller class, responsible for implementation of frequently used mechanisms.
    /// </summary>
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IResourceHandler _resourceHandler;
        private readonly IResourceAppService _resourceAppService;
        private readonly AppSettings _appSettings;

        protected ApiControllerBase(IResourceHandler resourceHandler, IResourceAppService resourceAppService, AppSettings appSettings)
        {
            _resourceAppService = resourceAppService;
            _resourceHandler = resourceHandler;
            _appSettings = appSettings;

            if (!_resourceHandler.CheckResourcesInitialized())
            {
                _resourceHandler.InitLanguage(_appSettings.EnvironmentSettings!.LanguageCode!);
                var resources = _resourceAppService.LoadAll(_resourceHandler.CurrentLanguage).GetAwaiter().GetResult();
                _resourceHandler.InitResources(resources);
            }
        }

        /// <summary>
        /// Method for handling validation of properties on data entering controller.
        /// </summary>
        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                var problem = ValidationProblem();
                if (problem is not null)
                {
                    var ex = new BadRequestException()
                    {
                        InternalExceptionCode = ApiInternalExceptionCode.BAD_REQUEST,
                        Errors = ValidationUtils.FormatValidationResponse(problem, ValidationFormat.JSON),
                    };
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Method to handle unexpected states of internal processing.
        /// </summary>
        /// <param name="ex">Handled exception coming from internal API logic.</param>
        protected StatusResultDto? ProcessApiException(Exception ex)
        {
            StatusResultDto? result = null;
            if (ex is ApiException)
            {
                var apiException = ex as ApiException;
                switch (apiException!.InternalExceptionCode)
                {
                    case ApiInternalExceptionCode.UNAUTHORIZED:
                        {
                            var responseDto = new UnauthorizedResultDto()
                            { Message = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.Unauthorized}" };
                            result = responseDto;
                        }
                        break;
                    case ApiInternalExceptionCode.NOT_FOUND:
                        {
                            var responseDto = new NotFoundResultDto()
                            { Message = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.NotFound}" };
                            result = responseDto;
                        }
                        break;
                    case ApiInternalExceptionCode.UNPROCESSABLE_ENTITY:
                        {
                            var responseDto = new UnprocessableResultDto()
                            {
                                Message = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.UnprocessableContent}"
                            };
                            result = responseDto;
                        }
                        break;
                    case ApiInternalExceptionCode.BAD_REQUEST:
                        {
                            var exceptionTyped = ex as BadRequestException;
                            var errors = ValidationUtils.ExtractValidationReasons(exceptionTyped?.Errors ?? new Dictionary<string, string[]>());
                            var responseDto = new BadRequestResultDto()
                            {
                                Errors = errors,
                                Message = $"{UIConstants.Base}.{UIConstants.Error}.{HttpStatusCode.BadRequest}"
                            };
                            result = responseDto;
                        }
                        break;
                    default:
                        break;
                }
                return result;
            }
            else
            {
                var responseDto = new InternalErrorResultDto()
                { Message = $"{ex.Message}" };

                result = responseDto;
                return result;
            }
        }

        /// <summary>
        /// Method to formulate various server responses.
        /// </summary>
        /// <param name="httpStatusCode">HTTP return code.</param>
        /// <param name="result">Result to send back to FE.</param>
        /// <returns>HTTP action result.</returns>
        protected IActionResult GetHttpResult(HttpStatusCode httpStatusCode, object? result = null, string? action = null)
        {
            return httpStatusCode switch
            {
                HttpStatusCode.Unauthorized => Unauthorized(result),
                HttpStatusCode.NotFound => NotFound(result),
                HttpStatusCode.BadRequest => BadRequest(result),
                HttpStatusCode.Created => CreatedAtAction(action, result),
                HttpStatusCode.InternalServerError => new ObjectResult(result) { StatusCode = (int)HttpStatusCode.InternalServerError },
                _ => result is null ? NoContent() : Ok(result),
            };
        }
    }
}
