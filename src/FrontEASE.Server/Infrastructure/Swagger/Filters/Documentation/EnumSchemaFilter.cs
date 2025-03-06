using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FrontEASE.Server.Infrastructure.Swagger.Filters.Documentation
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                var enumNames = Enum.GetNames(context.Type).Distinct().ToList();

                enumNames.ForEach(name =>
                {
                    if (!schema.Enum.Any(e => e.ToString() == name))
                    {
                        schema.Enum.Add(new OpenApiString(name));
                    }
                });
            }
        }
    }
}
