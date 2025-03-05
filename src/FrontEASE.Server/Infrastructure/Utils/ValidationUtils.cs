using FrontEASE.Server.Infrastructure.Utils.Enums;
using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Server.Infrastructure.Utils
{
    public static class ValidationUtils
    {
        public static IDictionary<string, string[]> FormatValidationResponse(IActionResult validationResponse, ValidationFormat format)
        {
            var problem = (validationResponse as BadRequestObjectResult)?.Value as ValidationProblemDetails;
            var errorsFormatted = new Dictionary<string, string[]>();

            if (problem is not null)
            {
                foreach (var validationErr in problem.Errors)
                {
                    var newEntryKey = string.Empty;
                    switch (format)
                    {
                        case ValidationFormat.DOTNET:
                            newEntryKey = ChangeInitialLetterSize(validationErr.Key, ValidationFormattingOperation.CAPITALIZE);
                            break;
                        case ValidationFormat.JSON:
                            newEntryKey = ChangeInitialLetterSize(validationErr.Key, ValidationFormattingOperation.LOWERCASE);
                            break;
                    }
                    errorsFormatted.Add(newEntryKey, validationErr.Value);
                }
            }
            return errorsFormatted;
        }

        public static IList<BadRequestErrorReason> ExtractValidationReasons(IDictionary<string, string[]> validationDictionary)
        {
            var listForm = validationDictionary.ToList();
            var errorsExtracted = new List<BadRequestErrorReason>();

            foreach (var keyValuePair in listForm)
            {
                var errorObject = new BadRequestErrorReason()
                {
                    Key = keyValuePair.Key,
                    Problems = [.. keyValuePair.Value]
                };
                errorsExtracted.Add(errorObject);
            }
            return errorsExtracted;
        }

        private static string ChangeInitialLetterSize(string fieldName, ValidationFormattingOperation operation)
        {
            var splitName = fieldName.Split('.').ToList();
            var formattedName = new List<string>();

            if (splitName?.Count > 0)
            {
                foreach (var namePart in splitName)
                {
                    var formattedPart = string.Empty;
                    if (!string.IsNullOrEmpty(namePart))
                    {
                        string firstLetter = string.Empty;
                        if (operation == ValidationFormattingOperation.LOWERCASE)
                        {
                            firstLetter = namePart.First().ToString().ToLower();
                        }
                        else if (operation == ValidationFormattingOperation.CAPITALIZE)
                        {
                            firstLetter = namePart.First().ToString().ToUpper();
                        }
                        formattedPart = namePart.Length == 1 ? firstLetter : $"{firstLetter}{namePart[1..]}";
                    }
                    formattedName.Add(formattedPart);
                }
                return string.Join(".", formattedName);
            }
            return string.Empty;
        }
    }
}
