using FoP_IMT.Shared.Infrastructure.Constants.Validations;
using FoP_IMT.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic
{
    public class Base64StringValidationAttribute : Base64StringAttribute
    {
        public Base64StringValidationAttribute()
        { }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
            if (serviceProvider is IResourceHandler resourceHandler)
            {
                var propertyName = validationContext.DisplayName;
                var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.Base64ValidStringValidationKey, [propertyName]);

                ErrorMessage = formattedErrorMessage;
            }

            return base.IsValid(value, validationContext) ?? ValidationResult.Success!;
        }
    }
}
