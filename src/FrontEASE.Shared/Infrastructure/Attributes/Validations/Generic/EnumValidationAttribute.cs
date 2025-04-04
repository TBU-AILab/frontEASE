using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic
{
    public class EnumValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;
        private readonly bool _isNullable;

        public EnumValidationAttribute(Type enumType)
        {
            _enumType = Nullable.GetUnderlyingType(enumType) ?? enumType;
            _isNullable = Nullable.GetUnderlyingType(enumType) is not null;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
            if (serviceProvider is IResourceHandler resourceHandler)
            {
                var propertyName = validationContext.DisplayName;

                if (_isNullable && value is null)
                {
                    return ValidationResult.Success!;
                }

                if (value is null || !Enum.IsDefined(_enumType, value))
                {
                    var stringVal = (value is null ? $"{null}" : value.ToString()) ?? $"{null}";

                    var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.EnumValidationKey, new[] { stringVal });
                    ErrorMessage = formattedErrorMessage;
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
