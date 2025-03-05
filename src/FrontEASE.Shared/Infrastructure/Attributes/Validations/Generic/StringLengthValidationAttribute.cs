using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic
{
    public class StringLengthValidationAttribute : StringLengthAttribute
    {
        private readonly bool _nullable;

        public StringLengthValidationAttribute(int minimumLength, int maximumLength, bool nullable = false) : base(maximumLength)
        {
            MinimumLength = minimumLength;
            _nullable = nullable;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (_nullable && (value is null || string.IsNullOrEmpty(value.ToString())))
            {
                return ValidationResult.Success!;
            }

            var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
            if (serviceProvider is IResourceHandler resourceHandler)
            {
                var minimumLength = MinimumLength;
                var maximumLength = MaximumLength;
                var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.StringLengthBetweenValidationKey, [minimumLength.ToString(), maximumLength.ToString()]);

                ErrorMessage = formattedErrorMessage;
            }

            return base.IsValid(value, validationContext) ?? ValidationResult.Success!;
        }
    }
}
