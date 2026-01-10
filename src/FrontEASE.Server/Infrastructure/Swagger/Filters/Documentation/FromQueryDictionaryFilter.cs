using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FrontEASE.Server.Infrastructure.Swagger.Filters.Documentation
{
    public class FromQueryDictionaryFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var description = context.ApiDescription;
            if (!(description.HttpMethod!.ToLower()).Equals(HttpMethod.Get.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                // We only want to do this for GET requests, if this is not a
                // GET request, leave this operation as is, do not modify
                return;
            }

            var actionParameters = description?.ActionDescriptor.Parameters;
            var apiParameters = description?.ParameterDescriptions.Where(p => p.Source.IsFromRequest).ToList();

            if (actionParameters?.Count == apiParameters?.Count)
            {
                // If no complex query parameters detected, leave this operation as is, do not modify
                return;
            }

            operation.Parameters = CreateParameters(actionParameters!, operation.Parameters, context);
        }

        private static IList<IOpenApiParameter>? CreateParameters(
            IList<ParameterDescriptor> actionParameters,
            IList<IOpenApiParameter>? operationParameters,
            OperationFilterContext context)
        {
            var newParameters = actionParameters
                .Select(p => CreateParameter(p, operationParameters, context))
                .Where(p => p is not null)
                .Select(p => p!)
                .ToList();

            return newParameters!.Count != 0 ? newParameters : null;
        }

        private static IOpenApiParameter? CreateParameter(
            ParameterDescriptor actionParameter,
            IList<IOpenApiParameter>? operationParameters,
            OperationFilterContext context)
        {
            var operationParamNames = operationParameters?.Select(p => p.Name);
            if (operationParamNames?.Contains(actionParameter.Name) == true)
            {
                // If param is defined as the action method argument, just pass it through
                return operationParameters?.First(p => p.Name == actionParameter.Name);
            }

            if (actionParameter.BindingInfo is null)
            {
                return null;
            }

            var generatedSchema = context.SchemaGenerator.GenerateSchema(actionParameter.ParameterType, context.SchemaRepository);

            var newParameter = new OpenApiParameter
            {
                Name = actionParameter.Name,
                In = ParameterLocation.Query,
                Schema = generatedSchema
            };

            return newParameter;
        }
    }
}
