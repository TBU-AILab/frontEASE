using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;
using FoP_IMT.Shared.Infrastructure.Constants.Validations;
using FoP_IMT.Shared.Infrastructure.Utils.Extensions;
using FoP_IMT.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FoP_IMT.Shared.Infrastructure.Attributes.Validations.Specific
{
    public class ParameterPropertyFilledValidationAttribute<T> : ValidationAttribute where T : class
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is not TaskModuleParameterValueDto dto)
            {
                return new ValidationResult("Invalid object type for validation.");
            }

            if (dto.Metadata?.Readonly == true)
            { return ValidationResult.Success!; }

            var properties = new Dictionary<string, object?>
            {
                { nameof(dto.FloatValue), dto.FloatValue },
                { nameof(dto.IntValue), dto.IntValue },
                { nameof(dto.StringValue), dto.StringValue },
                { nameof(dto.BoolValue), dto.BoolValue },
                { nameof(dto.EnumValue), dto.EnumValue }
            };

            if (!properties.Values.Any(v => v is not null && (v is not string str || !string.IsNullOrWhiteSpace(str))))
            {
                var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
                if (serviceProvider is IResourceHandler resourceHandler)
                {
                    var metadataName = dto.Metadata?.Name;
                    var propertyName = !string.IsNullOrWhiteSpace(metadataName) ? metadataName : validationContext.GetPropertyDisplayName<T>(resourceHandler);

                    var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterOneOfRequiredKey, [propertyName]);
                    ErrorMessage = formattedErrorMessage;
                    return new ValidationResult(ErrorMessage, [propertyName]);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
