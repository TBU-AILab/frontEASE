using FrontEASE.Server.Infrastructure.Swagger.Attributes;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace FrontEASE.Server.Infrastructure.Swagger.Filters.Operations
{
    public class ParameterIgnoreOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation?.Parameters is null || context?.MethodInfo is null) { return; }

            var ignoredParameterNames = context.MethodInfo.GetParameters()
                .Where(p => p.GetCustomAttribute<ParameterSwaggerIgnoreAttribute>() is not null)
                .Select(p => p.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (ignoredParameterNames.Count == 0) { return; }

            var parametersToRemove = operation.Parameters
                .Where(p => ignoredParameterNames.Contains(p.Name))
                .ToList();

            foreach (var parameter in parametersToRemove)
            {
                operation.Parameters.Remove(parameter);
            }
        }
    }
}
