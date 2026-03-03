namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Constants
{
    public static class ChartConstants
    {
        public const string ChartBackground = "transparent";
        public const string ChartForeColor = "var(--b-theme-text-primary)";
        public const string MutedColor = "var(--b-theme-text-muted)";
        public const string GridBorderColor = "var(--b-theme-background-light)";

        public const string TitleFontSize = "1.1rem";
        public const string SubtitleFontSize = "0.8rem";
        public const string LabelFormat = "n°{0}: \"{1}\"";

        public const int StrokeWidth = 2;
        public const int MarkerSize = 2;
        public const int MarkerStrokeWidth = 0;
        public const int MarkerHoverSizeOffset = 3;
        public const int GridStrokeDashArray = 4;

        public const int TitleOffsetY = 10;

        public const double GradientShadeIntensity = 1;
        public const double GradientOpacityFrom = 0.75;
        public const double GradientOpacityTo = 0.35;
        public const double GradientStopStart = 0;
        public const double GradientStopMiddle = 90;
        public const double GradientStopEnd = 100;

        public const decimal DefaultFitness = 0m;

        public const int MinPrecision = 2;
        public const int MaxPrecision = 6;
    }
}
