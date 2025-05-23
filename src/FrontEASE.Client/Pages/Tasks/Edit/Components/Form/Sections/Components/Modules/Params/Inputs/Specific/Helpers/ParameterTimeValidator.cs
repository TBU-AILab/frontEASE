﻿using Blazorise;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;

namespace FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Inputs.Specific.Helpers
{
    public class ParameterTimeValidator
    {
        public static void ValidateTimeParameter(ValidatorEventArgs args, TaskModuleParameterDto paramValue, IResourceHandler resourceHandler)
        {
            if (paramValue?.Value?.Metadata == null)
            {
                args.Status = ValidationStatus.Success;
                return;
            }

            var metadata = paramValue.Value.Metadata;
            var valueToValidate = paramValue.Value.IntValue;

            if (metadata.Type != ParameterType.TIME || metadata.Readonly)
            {
                args.Status = ValidationStatus.Success;
                return;
            }

            var parameterName = metadata.Name ?? resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}");

            if (metadata.Required == true)
            {
                if (!valueToValidate.HasValue)
                {
                    args.ErrorText = resourceHandler.GetValidationResource(ValidationKeyConstants.RequiredValidationKey, [parameterName]);
                    args.Status = ValidationStatus.Error;
                    return;
                }
                if (valueToValidate == -1)
                {
                    args.ErrorText = resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterTimeFormatKey, null);
                    args.Status = ValidationStatus.Error;
                    return;
                }
            }
            else
            {
                if (!valueToValidate.HasValue)
                {
                    args.Status = ValidationStatus.Success;
                    return;
                }

                if (valueToValidate == -1)
                {
                    args.ErrorText = resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterTimeFormatKey, null);
                    args.Status = ValidationStatus.Error;
                    return;
                }
            }

            if (valueToValidate.HasValue && valueToValidate.Value != -1)
            {
                var currentVal = valueToValidate.Value;
                var minValue = metadata.MinValue.HasValue ? (int)metadata.MinValue.Value : int.MinValue;
                var maxValue = metadata.MaxValue.HasValue ? (int)metadata.MaxValue.Value : int.MaxValue;

                var checkMin = minValue != int.MinValue;
                var checkMax = maxValue != int.MaxValue;

                if ((checkMin && currentVal < minValue) || (checkMax && currentVal > maxValue))
                {
                    var minDisplay = checkMin ? ParameterTimeHelper.ConvertSecondsToTimeString(minValue) : null;
                    var maxDisplay = checkMax ? ParameterTimeHelper.ConvertSecondsToTimeString(maxValue) : null;

                    args.ErrorText = resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterNumericRangeKey, [minDisplay ?? string.Empty, maxDisplay ?? string.Empty]);
                    args.Status = ValidationStatus.Error;
                    return;
                }
            }
        }
    }
}
