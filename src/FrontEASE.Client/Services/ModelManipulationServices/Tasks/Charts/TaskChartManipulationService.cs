using Blazorise.Charts;
using FrontEASE.Client.Infrastructure.Utils;
using FrontEASE.Client.Services.HelperServices.UI.Manage;
using FrontEASE.Client.Shared.Styling.Constants;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Messages;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Services.Resources;

namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts
{
    public class TaskChartManipulationService : ITaskChartManipulationService
    {
        private readonly IResourceHandler _resourceHandler;
        private readonly IUIManager _uiManager;

        public TaskChartManipulationService(
            IResourceHandler resourceHandler,
            IUIManager uiManager)
        {
            _resourceHandler = resourceHandler;
            _uiManager = uiManager;
        }

        public (IList<float> Values, IList<string> Labels) GetValueEvolutionChartData(IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions)
        {
            var labels = new List<string>();
            var values = new List<float>();

            var i = 0;
            foreach (var solution in solutions)
            {
                var message = messages.FirstOrDefault(m => m.ID == solution.TaskMessageID);

                var fitness = (float)(solution.Fitness ?? float.NegativeInfinity);
                values.Add(fitness);

                var messageContent = string.IsNullOrEmpty(message?.Content) ? _resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}") : message.Content;
                labels.Add($"n°{++i}: \"{TextHelper.TruncateString(messageContent, TextConstants.TaskChartMessageDisplayLength)}\"");
            }

            return new(values, [.. labels]);
        }

        public (IList<float> Values, IList<string> Labels) GetValueConvergenceChartData(IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions)
        {
            var labels = new List<string>();
            var values = new List<float>();

            var bestSoFar = float.NegativeInfinity;
            var i = 0;
            foreach (var solution in solutions)
            {
                var message = messages.FirstOrDefault(m => m.ID == solution.TaskMessageID);

                var fitness = (float)(solution.Fitness ?? float.NegativeInfinity);
                bestSoFar = Math.Max(bestSoFar, fitness);
                values.Add(bestSoFar);

                var messageContent = string.IsNullOrEmpty(message?.Content) ? _resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}") : message.Content;
                labels.Add($"n°{++i}: \"{TextHelper.TruncateString(messageContent, TextConstants.TaskChartMessageDisplayLength)}\"");
            }

            return new(values, [.. labels]);
        }

        public LineChartOptions PrepareLineChartOptions(string titleText, string subtitleText)
        {
            var options = new LineChartOptions()
            {
                MaintainAspectRatio = true,
                Responsive = true,
                Plugins = new()
                {
                    Legend = new()
                    {
                        FullSize = true,
                        Display = true,
                        Title = new()
                        {
                            Text = titleText,
                            Display = true,
                            Font = new()
                            {
                                Style = "italic"
                            }
                        }
                    },
                    Subtitle = new()
                    {
                        Text = subtitleText,
                        Color = new List<string>() { ChartColor.FromHtmlColorCode(_uiManager.Theme.ColorOptions.Primary) },
                        Display = true,
                    }
                },
                Scales = new()
                {
                    X = new()
                    {
                        Display = false
                    }
                }
            };

            return options;
        }
    }
}
