using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic
{
    public class DateNotInFutureValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
            if (serviceProvider is IResourceHandler resourceHandler)
            {
                if (value is DateTime dateValue && dateValue > DateTime.Now)
                {
                    var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.DateNotInFutureValidationKey, [dateValue.ToString("d")]);
                    ErrorMessage = formattedErrorMessage;
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
