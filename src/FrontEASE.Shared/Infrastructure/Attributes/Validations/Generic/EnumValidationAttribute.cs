using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic
{
    public class EnumValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumValidationAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
            if (serviceProvider is IResourceHandler resourceHandler)
            {
                var propertyName = validationContext.DisplayName;

                if (value is null || !Enum.IsDefined(_enumType, value))
                {
                    var stringVal = (value is null ? $"{null}" : value.ToString()) ?? $"{null}";

                    var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.EnumValidationKey, [stringVal]);
                    ErrorMessage = formattedErrorMessage;
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
