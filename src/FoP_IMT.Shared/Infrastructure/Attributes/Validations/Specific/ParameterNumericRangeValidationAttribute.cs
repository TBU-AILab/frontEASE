using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;
using FoP_IMT.Shared.Data.Enums.Tasks.Config.Modules.Parameters;
using FoP_IMT.Shared.Infrastructure.Constants.Validations;
using FoP_IMT.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FoP_IMT.Shared.Infrastructure.Attributes.Validations.Specific
{
    namespace FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic
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

                if (dto.Metadata is null || dto.Metadata.Readonly || (dto.Metadata.Type != ParameterType.INT && dto.Metadata.Type != ParameterType.FLOAT))
                { return ValidationResult.Success!; }

                if (_nullable && (value is null || string.IsNullOrEmpty(value.ToString())))
                { return ValidationResult.Success!; }

                var minValue = dto.Metadata.MinValue ?? (dto.Metadata.Type == ParameterType.INT ? int.MinValue : float.MinValue);
                var maxValue = dto.Metadata.MaxValue ?? (dto.Metadata.Type == ParameterType.INT ? int.MaxValue : float.MaxValue);

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
