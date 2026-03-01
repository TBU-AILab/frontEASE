namespace FrontEASE.Server.Infrastructure.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field)]
    public class ParameterSwaggerIgnoreAttribute : Attribute
    {
    }
}
