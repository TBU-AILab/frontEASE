using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.Enum;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;
using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Specific.Tasks
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
                .Select(e => e.StringValue!);

            var validModuleShortNames = enumOptions
                .Where(e => e.ModuleValue is not null)
                .Select(e => e.ModuleValue!.ShortName);

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
