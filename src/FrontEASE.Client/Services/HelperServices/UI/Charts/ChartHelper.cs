using ApexCharts;
using FrontEASE.Client.Services.HelperServices.UI.Manage;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Constants;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Models;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;

namespace FrontEASE.Client.Services.HelperServices.UI.Charts
{
    public class ChartHelper(IUIManager uiManager) : IChartHelper
    {
        public ApexChartOptions<ChartDataPoint> PrepareLineChartOptions(string titleText, string subtitleText)
        {
            var options = new ApexChartOptions<ChartDataPoint>
            {
                Chart = new Chart
                {
                    Background = ChartConstants.ChartBackground,
                    ForeColor = ChartConstants.ChartForeColor,
                    Toolbar = new Toolbar { Show = false },
                },
                Title = new Title
                {
                    Text = subtitleText,
                    Align = Align.Center,
                    OffsetY = ChartConstants.TitleOffsetY,
                    Style = new TitleStyle
                    {
                        Color = uiManager.Theme.ColorOptions.Primary,
                        FontSize = ChartConstants.TitleFontSize
                    }
                },
                Subtitle = new Subtitle
                {
                    Text = titleText,
                    Align = Align.Center,
                    Style = new SubtitleStyle
                    {
                        Color = ChartConstants.MutedColor,
                        FontSize = ChartConstants.SubtitleFontSize
                    }
                },
                Xaxis = new XAxis
                {
                    Labels = new XAxisLabels
                    {
                        Show = false,
                    }
                },
                Yaxis =
                [
                    new YAxis
                    {
                        Labels = new YAxisLabels
                        {
                            Style = new AxisLabelStyle { Colors = ChartConstants.MutedColor }
                        }
                    }
                ],
                Stroke = new Stroke
                {
                    Curve = Curve.Straight,
                    Width = ChartConstants.StrokeWidth,
                },
                Colors = [uiManager.Theme.ColorOptions.Primary],
                Fill = new Fill
                {
                    Type = [FillType.Gradient],
                    Gradient = new FillGradient
                    {
                        ShadeIntensity = ChartConstants.GradientShadeIntensity,
                        OpacityFrom = ChartConstants.GradientOpacityFrom,
                        OpacityTo = ChartConstants.GradientOpacityTo,
                        Stops = [ChartConstants.GradientStopStart, ChartConstants.GradientStopMiddle, ChartConstants.GradientStopEnd]
                    }
                },
                Markers = new Markers
                {
                    Size = ChartConstants.MarkerSize,
                    Colors = [uiManager.Theme.ColorOptions.Success],
                    StrokeColors = [uiManager.Theme.ColorOptions.Success],
                    StrokeWidth = ChartConstants.MarkerStrokeWidth,
                    Hover = new MarkersHover { SizeOffset = ChartConstants.MarkerHoverSizeOffset }
                },
                Legend = new Legend { Show = true },
                Grid = new Grid
                {
                    BorderColor = ChartConstants.GridBorderColor,
                    StrokeDashArray = ChartConstants.GridStrokeDashArray
                },
                Tooltip = new Tooltip
                {
                    Theme = Mode.Dark
                },
                Theme = new Theme
                {
                    Mode = Mode.Dark
                }
            };

            return options;
        }

        public int DetermineDecimalPrecision(IList<TaskSolutionDto> solutions)
        {
            var fitnessValues = solutions.Select(s => (decimal)(s.Fitness ?? (double)ChartConstants.DefaultFitness)).ToList();
            if (fitnessValues.Count < 2)
            { return ChartConstants.MinPrecision; }

            var uniqueValues = fitnessValues.Distinct().OrderBy(v => v).ToList();
            if (uniqueValues.Count < 2)
            { return ChartConstants.MinPrecision; }

            var minDiff = decimal.MaxValue;
            for (var i = 1; i < uniqueValues.Count; i++)
            {
                var diff = uniqueValues[i] - uniqueValues[i - 1];
                if (diff > 0 && diff < minDiff)
                {
                    minDiff = diff;
                }
            }

            if (minDiff == decimal.MaxValue)
            { return ChartConstants.MinPrecision; }

            var precision = (int)Math.Ceiling(-Math.Log10((double)minDiff)) + 1;
            return Math.Clamp(precision, ChartConstants.MinPrecision, ChartConstants.MaxPrecision);
        }
    }
}
