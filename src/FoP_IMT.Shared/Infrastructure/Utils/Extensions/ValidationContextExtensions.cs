using FoP_IMT.Shared.Data.Enums.Shared.General;
using FoP_IMT.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FoP_IMT.Shared.Infrastructure.Utils.Extensions
{
    public static class ValidationContextExtensions
    {
        public static string GetPropertyDisplayName<T>(this ValidationContext validationContext, IResourceHandler resourceHandler) where T : class
        {
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName!);

            if (property is not null)
            {
                var resourceKey = AttributeExtensions.GetResourceFieldValue<T>(validationContext.MemberName!, PropertyDisplayResourceType.FIELD);
                return resourceHandler.GetResource(resourceKey);
            }

            return validationContext.DisplayName ?? validationContext.MemberName ?? "N/A";
        }
    }
}
