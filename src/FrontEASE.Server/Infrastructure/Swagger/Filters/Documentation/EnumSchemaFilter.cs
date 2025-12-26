using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FrontEASE.Server.Infrastructure.Swagger.Filters.Documentation
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum?.Clear();
                var enumNames = Enum.GetNames(context.Type).Distinct();

                foreach (var enumName in enumNames)
                {
                    if (!schema.Enum!.Any(e => e.ToString() == enumName))
                    {
                        schema.Enum?.Add(enumName);
                    }
                }
            }
        }
    }
}
