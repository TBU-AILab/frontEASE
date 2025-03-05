using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;

namespace FrontEASE.Shared.Infrastructure.Constants.Validations
{
    public static class ValidationKeyConstants
    {
        /* Generic */
        public const string CollectionNotEmptyValidationKey = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.CollectionNotEmpty}";
        public const string RequiredValidationKey = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.Required}";
        public const string EmailValidationKey = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.Email}";
        public const string EnumValidationKey = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.InvalidValue}";
        public const string StringLengthBetweenValidationKey = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.TextLengthBetween}";
        public const string Base64ValidStringValidationKey = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.Base64String}";
        public const string NumericRangeValidationKey = $"{UIConstants.Data}.{UIConstants.Generic}.{UIStateConstants.Validation}.{UIValidationConstants.NumericRange}";

        /* Specific */
        public const string ParameterOneOfRequiredKey = $"{UIConstants.Data}.{UIConstants.Specific}.{UIStateConstants.Validation}.{UIValidationConstants.ParameterOneOfRequired}";
        public const string ParameterOneOfEnumValuesKey = $"{UIConstants.Data}.{UIConstants.Specific}.{UIStateConstants.Validation}.{UIValidationConstants.ParameterOneOfEnumValues}";
        public const string ParameterNumericRangeKey = $"{UIConstants.Data}.{UIConstants.Specific}.{UIStateConstants.Validation}.{UIValidationConstants.ParameterNumericRange}";
    }
}
