using FrontEASE.Client.Infrastructure.Utils;
using FrontEASE.Client.Services.HelperServices.UI.Charts;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Constants;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Models;
using FrontEASE.Client.Shared.Styling.Constants;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Messages;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;
using FrontEASE.Shared.Data.Enums.Tasks.Config;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Services.Resources;

namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts
{
    public class TaskChartManipulationService(
        IResourceHandler resourceHandler,
        IChartHelper chartHelper) : ITaskChartManipulationService
    {
        public IList<ChartDataPoint> GetValueEvolutionChartData(OptimizationGoalType optimizationGoal, IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions)
        {
            var dataPoints = new List<ChartDataPoint>(solutions.Count);
            var precision = chartHelper.DetermineDecimalPrecision(solutions);

            var messagesLookup = messages.ToDictionary(m => m.ID);
            var fallbackContent = resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}");

            var i = 0;
            foreach (var solution in solutions)
            {
                messagesLookup.TryGetValue(solution.TaskMessageID, out var message);

                var fitness = Math.Round((decimal)(solution.Fitness ?? (double)ChartConstants.DefaultFitness), precision);
                var messageContent = string.IsNullOrEmpty(message?.Content) ? fallbackContent : message.Content;
                var label = string.Format(ChartConstants.LabelFormat, ++i, TextHelper.TruncateString(messageContent, TextConstants.TaskChartMessageDisplayLength));

                dataPoints.Add(new ChartDataPoint { Order = i, Label = label, Value = fitness });
            }

            return dataPoints;
        }

        public IList<ChartDataPoint> GetValueConvergenceChartData(OptimizationGoalType optimizationGoal, IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions)
        {
            var dataPoints = new List<ChartDataPoint>(solutions.Count);
            var precision = chartHelper.DetermineDecimalPrecision(solutions);

            var messagesLookup = messages.ToDictionary(m => m.ID);
            var fallbackContent = resourceHandler.GetResource($"{UIConstants.Data}.{UIConstants.Generic}.{UIValueConstants.NotAvailable}");

            var bestSoFar = optimizationGoal == OptimizationGoalType.MAXIMIZATION ? decimal.MinValue : decimal.MaxValue;
            var i = 0;
            foreach (var solution in solutions)
            {
                messagesLookup.TryGetValue(solution.TaskMessageID, out var message);

                var fitness = Math.Round((decimal)(solution.Fitness ?? (double)ChartConstants.DefaultFitness), precision);
                bestSoFar = optimizationGoal == OptimizationGoalType.MAXIMIZATION ? Math.Max(bestSoFar, fitness) : Math.Min(bestSoFar, fitness);
                var messageContent = string.IsNullOrEmpty(message?.Content) ? fallbackContent : message.Content;
                var label = string.Format(ChartConstants.LabelFormat, ++i, TextHelper.TruncateString(messageContent, TextConstants.TaskChartMessageDisplayLength));

                dataPoints.Add(new ChartDataPoint { Order = i, Label = label, Value = bestSoFar });
            }

            return dataPoints;
        }
    }
}
