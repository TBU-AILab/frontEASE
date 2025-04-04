using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic
{
    public class DateRangeValidationAttribute : ValidationAttribute
    {
        private readonly DateTime? _from;
        private readonly DateTime? _to;

        public DateRangeValidationAttribute(string? from = null, string? to = null)
        {
            if (!string.IsNullOrEmpty(from))
            { _from = DateTime.Parse(from); }

            if (!string.IsNullOrEmpty(to))
            { _to = DateTime.Parse(to); }
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
            if (serviceProvider is IResourceHandler resourceHandler)
            {
                if (value is DateTime dateValue)
                {
                    if (_from.HasValue && !_to.HasValue && dateValue < _from.Value)
                    {
                        var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.DateFromValidationKey, [_from.Value.ToString("dd.MM.yyyy")]);
                        ErrorMessage = formattedErrorMessage;
                        return new ValidationResult(ErrorMessage);
                    }

                    if (!_from.HasValue && _to.HasValue && dateValue > _to.Value)
                    {
                        var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.DateToValidationKey, [_to.Value.ToString("dd.MM.yyyy")]);
                        ErrorMessage = formattedErrorMessage;
                        return new ValidationResult(ErrorMessage);
                    }

                    if (_from.HasValue && _to.HasValue && (dateValue < _from.Value || dateValue > _to.Value))
                    {
                        var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.DateBetweenValidationKey, [_from.Value.ToString("dd.MM.yyyy"), _to.Value.ToString("dd.MM.yyyy")]);
                        ErrorMessage = formattedErrorMessage;
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }

            return ValidationResult.Success!;
        }
    }
}