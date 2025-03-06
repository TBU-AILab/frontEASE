using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic
{
    public class RangeValidationAttribute : RangeAttribute
    {
        private readonly bool _nullable;

        public RangeValidationAttribute(int minimum, int maximum, bool nullable = false) : base(minimum, maximum)
        {
            _nullable = nullable;
        }

        public RangeValidationAttribute(double minimum, double maximum, bool nullable = false) : base(minimum, maximum)
        {
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
                var min = this.Minimum;
                var max = this.Maximum;
                var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.NumericRangeValidationKey, [min.ToString()!, max.ToString()!]);

                ErrorMessage = formattedErrorMessage;
            }

            return base.IsValid(value, validationContext) ?? ValidationResult.Success!;
        }
    }
}
