using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FrontEASE.Server.Infrastructure.Swagger.Filters.Schema
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                if (schema.Enum is null)
                {
                    if (schema is OpenApiSchema concreteSchema) { concreteSchema.Enum = []; }
                    else { return; }
                }

                schema.Enum!.Clear();
                var enumNames = Enum.GetNames(context.Type).Distinct();

                foreach (var enumName in enumNames)
                {
                    if (!schema.Enum.Any(e => e.ToString() == enumName))
                    {
                        schema.Enum.Add(enumName);
                    }
                }
            }
        }
    }
}
