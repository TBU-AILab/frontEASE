using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Shared.Infrastructure.Constants.Validations;
using FoP_IMT.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FoP_IMT.Shared.Infrastructure.Attributes.Validations.Specific
{
    public class ParameterEnumPropertyFilledValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is not TaskModuleParameterEnumOptionDto option)
            {
                return new ValidationResult("Invalid object type for validation.");
            }

            if (option.Metadata?.Readonly == true)
            { return ValidationResult.Success!; }

            var hasStringValue = !string.IsNullOrWhiteSpace(option.StringValue);
            var hasModuleShortName = option.ModuleValue is not null && !string.IsNullOrWhiteSpace(option.ModuleValue.ShortName);

            if (!hasStringValue && !hasModuleShortName)
            {
                var metadataName = option.Metadata?.Name ?? validationContext.MemberName;

                var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
                if (serviceProvider is IResourceHandler resourceHandler)
                {
                    var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterOneOfRequiredKey,[metadataName!]);

                    ErrorMessage = formattedErrorMessage;
                    return new ValidationResult(ErrorMessage, [metadataName!]);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
