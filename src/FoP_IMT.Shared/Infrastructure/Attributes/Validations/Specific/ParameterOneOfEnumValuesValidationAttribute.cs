using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Shared.Data.Enums.Tasks.Config.Modules.Parameters;
using FoP_IMT.Shared.Infrastructure.Constants.Validations;
using FoP_IMT.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FoP_IMT.Shared.Infrastructure.Attributes.Validations.Specific
{
    public class ParameterOneOfEnumValuesValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is not TaskModuleParameterEnumOptionDto option)
            {
                return new ValidationResult("Invalid object type for validation.");
            }

            var metadata = option.Metadata;
            if (metadata is null || metadata.Readonly || metadata.Type != ParameterType.ENUM)
            { return ValidationResult.Success!; }

            var enumOptions = metadata.EnumOptions;
            if (enumOptions is null || !(enumOptions.Count > 0))
            { return ValidationResult.Success!; }

            var validStringValues = enumOptions
                .Where(e => e.StringValue is not null)
                .Select(e => e.StringValue!)
                .ToList();

            var validModuleShortNames = enumOptions
                .Where(e => e.ModuleValue is not null)
                .Select(e => e.ModuleValue!.ShortName)
                .ToList();

            var parameterName = metadata.Name ?? validationContext.MemberName;

            if (validationContext.MemberName == nameof(TaskModuleParameterEnumOptionDto.StringValue))
            {
                if (value is string stringValue && !validStringValues.Contains(stringValue))
                {
                    var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
                    if (serviceProvider is IResourceHandler resourceHandler)
                    {
                        var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterOneOfEnumValuesKey, [string.Join(", ", validStringValues)]);

                        ErrorMessage = formattedErrorMessage;
                        return new ValidationResult(ErrorMessage, [parameterName!]);
                    }
                }
            }

            if (validationContext.MemberName == nameof(TaskModuleParameterEnumOptionDto.ModuleValue))
            {
                if (value is TaskModuleDto moduleValue && !validModuleShortNames.Contains(moduleValue.ShortName))
                {
                    var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
                    if (serviceProvider is IResourceHandler resourceHandler)
                    {
                        var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterOneOfEnumValuesKey, [string.Join(", ", validModuleShortNames)]);

                        ErrorMessage = formattedErrorMessage;
                        return new ValidationResult(ErrorMessage, [parameterName!]);
                    }
                }
            }

            return ValidationResult.Success!;
        }
    }
}
