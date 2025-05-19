using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Services.Resources;

namespace FrontEASE.Client.Pages.Tasks.Edit.Components.Form.Sections.Components.Modules.Params.Inputs.Specific.Helpers
{
    public static class ParameterTimeHelper
    {
        public static string? ConvertSecondsToTimeString(int? totalSeconds)
        {
            if (!totalSeconds.HasValue || totalSeconds.Value == -1)
            {
                return null;
            }

            if (totalSeconds < 0) totalSeconds = 0;

            var hours = totalSeconds.Value / 3600;
            var minutes = (totalSeconds.Value % 3600) / 60;
            var seconds = totalSeconds.Value % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public static string FormatTimeValueForDisplay(int? totalSeconds, IResourceHandler resourceHandler)
        {
            if (!totalSeconds.HasValue || totalSeconds.Value == -1)
            {
                return resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}");
            }

            if (totalSeconds.Value < 0)
            {
                return resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}");
            }

            var ts = totalSeconds.Value;
            var hours = ts / 3600;
            var minutes = (ts % 3600) / 60;
            var seconds = ts % 60;

            return $"{hours} h : {minutes} m : {seconds} s";
        }
    }
}
