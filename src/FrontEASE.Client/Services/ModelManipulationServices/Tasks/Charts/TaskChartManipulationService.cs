using ApexCharts;
using FrontEASE.Client.Infrastructure.Utils;
using FrontEASE.Client.Services.HelperServices.UI.Manage;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Constants;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Models;
using FrontEASE.Client.Shared.Styling.Constants;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Messages;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Services.Resources;

namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts
{
    public class TaskChartManipulationService(
        IResourceHandler resourceHandler,
        IUIManager uiManager) : ITaskChartManipulationService
    {
        public IList<ChartDataPoint> GetValueEvolutionChartData(IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions)
        {
            var dataPoints = new List<ChartDataPoint>();

            var i = 0;
            foreach (var solution in solutions)
            {
                var message = messages.FirstOrDefault(m => m.ID == solution.TaskMessageID);

                var fitness = (decimal)(solution.Fitness ?? (double)ChartConstants.DefaultFitness);
                var messageContent = string.IsNullOrEmpty(message?.Content) ? resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}") : message.Content;
                var label = string.Format(ChartConstants.LabelFormat, ++i, TextHelper.TruncateString(messageContent, TextConstants.TaskChartMessageDisplayLength));

                dataPoints.Add(new ChartDataPoint { Order = i, Label = label, Value = fitness });
            }

            return dataPoints;
        }

        public IList<ChartDataPoint> GetValueConvergenceChartData(IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions)
        {
            var dataPoints = new List<ChartDataPoint>();

            var bestSoFar = ChartConstants.DefaultFitness;
            var i = 0;
            foreach (var solution in solutions)
            {
                var message = messages.FirstOrDefault(m => m.ID == solution.TaskMessageID);

                var fitness = (decimal)(solution.Fitness ?? (double)ChartConstants.DefaultFitness);
                bestSoFar = Math.Max(bestSoFar, fitness);
                var messageContent = string.IsNullOrEmpty(message?.Content) ? resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}") : message.Content;
                var label = string.Format(ChartConstants.LabelFormat, ++i, TextHelper.TruncateString(messageContent, TextConstants.TaskChartMessageDisplayLength));

                dataPoints.Add(new ChartDataPoint { Order = i, Label = label, Value = bestSoFar });
            }

            return dataPoints;
        }

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
                    Curve = Curve.Smooth,
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
    }
}
