using Blazorise.Charts;

namespace FrontEASE.Client.Infrastructure.Utils
{
    public static class ColorHelper
    {
        public static string HtmlToChartRGBA(string hex, float alpha)
        {
            if (string.IsNullOrWhiteSpace(hex))
            {
                return ChartColor.FromRgba(0, 0, 0, alpha);
            }

            var trimmed = hex.TrimStart('#');
            if (trimmed.Length != 6)
            {
                return ChartColor.FromRgba(0, 0, 0, alpha);
            }

            var r = (byte)Convert.ToInt32(trimmed[..2], 16);
            var g = (byte)Convert.ToInt32(trimmed.Substring(2, 2), 16);
            var b = (byte)Convert.ToInt32(trimmed.Substring(4, 2), 16);

            return ChartColor.FromRgba(r, g, b, alpha);
        }
    }
}
