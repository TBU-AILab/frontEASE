﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace FrontEASE.Server.Infrastructure.Swagger.Filters.Documentation
{
    public class AuthResponsesFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context!.MethodInfo!.DeclaringType!.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
                operation.Responses.Add
                (
                    ((int)HttpStatusCode.Unauthorized).ToString(),
                    new OpenApiResponse
                    {
                        Description = HttpStatusCode.Unauthorized.ToString()
                    }
                );
        }
    }
}
