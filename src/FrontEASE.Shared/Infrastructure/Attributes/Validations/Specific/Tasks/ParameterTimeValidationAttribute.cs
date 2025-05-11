using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Specific.Tasks
{
    public class ParameterTimeValidationAttribute : ValidationAttribute
    {
        private static string? ConvertSecondsToTimeStringForDisplay(int? totalSeconds)
        {
            if (!totalSeconds.HasValue) return null;
            if (totalSeconds < 0) totalSeconds = 0;

            var hours = totalSeconds.Value / 3600;
            var minutes = (totalSeconds.Value % 3600) / 60;
            var seconds = totalSeconds.Value % 60;
            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is not TaskModuleParameterValueDto dto)
            {
                return new ValidationResult("Invalid object type for validation.");
            }

            if ((dto.Metadata is null || dto.Metadata.Type != ParameterType.TIME) || dto.Metadata.Readonly)
            {
                return ValidationResult.Success;
            }

            var resourceHandler = validationContext.GetService(typeof(IResourceHandler)) as IResourceHandler;
            if (resourceHandler is not null)
            {
                var parameterName = dto.Metadata.Name ?? resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}" ?? validationContext.DisplayName);
                var totalSecondsFromIntValue = (int?)value;

                if (dto.Metadata.Required == true)
                {
                    if (!totalSecondsFromIntValue.HasValue)
                    { return new ValidationResult(resourceHandler.GetValidationResource(ValidationKeyConstants.RequiredValidationKey, [parameterName]), [validationContext.MemberName!]); }

                    if (totalSecondsFromIntValue == -1)
                    { return new ValidationResult(resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterTimeFormatKey, null)); }
                }
                else
                {
                    if (!totalSecondsFromIntValue.HasValue)
                    { return ValidationResult.Success; }
                    if (totalSecondsFromIntValue == -1)
                    { return new ValidationResult(resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterTimeFormatKey, null)); }
                }

                if (totalSecondsFromIntValue.HasValue && totalSecondsFromIntValue.Value != -1)
                {
                    var currentVal = totalSecondsFromIntValue.Value;
                    var minValue = dto.Metadata.MinValue.HasValue ? (int)dto.Metadata.MinValue.Value : int.MinValue;
                    var maxValue = dto.Metadata.MaxValue.HasValue ? (int)dto.Metadata.MaxValue.Value : int.MaxValue;

                    if (currentVal < minValue || currentVal > maxValue)
                    {
                        var minDisplay = ConvertSecondsToTimeStringForDisplay(minValue);
                        var maxDisplay = ConvertSecondsToTimeStringForDisplay(maxValue);

                        return new ValidationResult(resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterNumericRangeKey, [minDisplay ?? string.Empty, maxDisplay ?? string.Empty]));
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}