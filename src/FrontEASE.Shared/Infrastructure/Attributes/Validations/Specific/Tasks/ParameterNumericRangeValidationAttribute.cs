using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;
using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Specific
{
    namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic
    {
        public class ParameterNumericRangeValidationAttribute : ValidationAttribute
        {
            private readonly bool _nullable;

            public ParameterNumericRangeValidationAttribute(bool nullable = false)
            {
                _nullable = nullable;
            }

            protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
            {
                if (validationContext.ObjectInstance is not TaskModuleParameterValueDto dto)
                {
                    return new ValidationResult("Invalid object type for validation.");
                }

                var validatableTypes = new List<ParameterType>() { ParameterType.INT, ParameterType.FLOAT, ParameterType.TIME };
                var integerTypes = new List<ParameterType>() { ParameterType.INT, ParameterType.TIME };

                if (dto.Metadata is null || dto.Metadata.Readonly || (!validatableTypes.Any(x => x == dto.Metadata.Type)))
                { return ValidationResult.Success!; }

                if (_nullable && (value is null || string.IsNullOrEmpty(value.ToString())))
                { return ValidationResult.Success!; }

                var minValue = dto.Metadata.MinValue ?? (integerTypes.Contains(dto.Metadata.Type) ? int.MinValue : float.MinValue);
                var maxValue = dto.Metadata.MaxValue ?? (integerTypes.Contains(dto.Metadata.Type) ? int.MaxValue : float.MaxValue);

                if (value is double doubleValue && (doubleValue < minValue || doubleValue > maxValue) ||
                    value is int intValue && (intValue < minValue || intValue > maxValue))
                {
                    var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
                    if (serviceProvider is IResourceHandler resourceHandler)
                    {
                        var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterNumericRangeKey, [minValue.ToString(), maxValue.ToString()]);
                        ErrorMessage = formattedErrorMessage;
                    }

                    var propertyName = dto.Metadata.Name ?? validationContext.DisplayName;
                    return new ValidationResult(ErrorMessage, [propertyName]);
                }

                return ValidationResult.Success!;
            }
        }
    }
}
