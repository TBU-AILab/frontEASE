using FoP_IMT.Shared.Infrastructure.Constants.Validations;
using FoP_IMT.Shared.Services.Resources;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic
{
    public class CollectionNotEmptyValidationAttribute : ValidationAttribute
    {
        public CollectionNotEmptyValidationAttribute()
        { }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
            if (serviceProvider is IResourceHandler resourceHandler)
            {
                var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.CollectionNotEmptyValidationKey, []);
                ErrorMessage = formattedErrorMessage;
            }

            if (value is null)
            {
                return new ValidationResult(ErrorMessage);
            }
            else if (value is ICollection collection && collection.Count == 0)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}
