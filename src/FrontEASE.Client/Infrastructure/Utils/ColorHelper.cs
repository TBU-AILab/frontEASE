namespace FrontEASE.Client.Infrastructure.Utils
{
    public static class ColorHelper
    {
        public static string HtmlToRGBA(string hex, float alpha)
        {
            if (string.IsNullOrWhiteSpace(hex))
            {
                return $"rgba(0, 0, 0, {alpha})";
            }

            var trimmed = hex.TrimStart('#');
            if (trimmed.Length != 6)
            {
                return $"rgba(0, 0, 0, {alpha})";
            }

            var r = (byte)Convert.ToInt32(trimmed[..2], 16);
            var g = (byte)Convert.ToInt32(trimmed.Substring(2, 2), 16);
            var b = (byte)Convert.ToInt32(trimmed.Substring(4, 2), 16);

            return $"rgba({r}, {g}, {b}, {alpha})";
        }
    }
}
