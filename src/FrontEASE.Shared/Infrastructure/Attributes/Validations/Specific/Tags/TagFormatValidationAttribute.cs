using FrontEASE.Shared.Infrastructure.Constants.Validations;
using FrontEASE.Shared.Services.Resources;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FrontEASE.Shared.Infrastructure.Attributes.Validations.Specific.Tags
{
    public partial class TagFormatValidationAttribute : ValidationAttribute
    {
        private readonly Regex _regexPattern = TagFormatRegex();
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetService(typeof(IResourceHandler));
            if (serviceProvider is IResourceHandler resourceHandler)
            {
                if (value is null || string.IsNullOrEmpty(value.ToString()))
                {
                    return ValidationResult.Success!;
                }

                if (!_regexPattern.IsMatch(value.ToString()!))
                {
                    var formattedErrorMessage = resourceHandler.GetValidationResource(ValidationKeyConstants.ParameterTagFormatKey, [(value as string) ?? $"{null}"]);
                    ErrorMessage = formattedErrorMessage;
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }

        [GeneratedRegex(@"^[A-Z0-9]+([_\-][A-Z0-9]+)*$", RegexOptions.Compiled)]
        private static partial Regex TagFormatRegex();
    }
}
