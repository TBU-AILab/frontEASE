using ApexCharts;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Models;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Messages;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;
using FrontEASE.Shared.Data.Enums.Tasks.Config;

namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts
{
    public interface ITaskChartManipulationService
    {
        IList<ChartDataPoint> GetValueEvolutionChartData(OptimizationGoalType optimizationGoal, IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions);
        IList<ChartDataPoint> GetValueConvergenceChartData(OptimizationGoalType optimizationGoal, IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions);
    }
}
