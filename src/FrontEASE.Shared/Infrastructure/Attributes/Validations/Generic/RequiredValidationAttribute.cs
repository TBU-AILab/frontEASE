using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Infrastructure.Utils.Extensions;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic
{
    public class RequiredValidationAttribute<T> : RequiredAttribute where T : class
    {
        public RequiredValidationAttribute()
        { }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
            if (serviceProvider is IResourceHandler resourceHandler)
            {
                var propertyName = validationContext.GetPropertyDisplayName<T>(resourceHandler);
                var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.RequiredValidationKey, [propertyName]);

                ErrorMessage = formattedErrorMessage;
            }

            return base.IsValid(value, validationContext) ?? ValidationResult.Success!;
        }
    }
}
