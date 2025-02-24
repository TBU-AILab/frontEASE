using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FoP_IMT.Server.Infrastructure.Swagger.Filters.Documentation
{
    public class AddSchemaDocumentFilter<T> : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var taskCoreDtoType = typeof(T);

            if (!context.SchemaRepository.Schemas.ContainsKey(taskCoreDtoType.Name))
            {
                context.SchemaGenerator.GenerateSchema(taskCoreDtoType, context.SchemaRepository);
            }
        }
    }
}
