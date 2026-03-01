using ApexCharts;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Models;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Messages;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;

namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts
{
    public interface ITaskChartManipulationService
    {
        IList<ChartDataPoint> GetValueEvolutionChartData(IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions);
        IList<ChartDataPoint> GetValueConvergenceChartData(IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions);
        ApexChartOptions<ChartDataPoint> PrepareLineChartOptions(string titleText, string subtitleText);
    }
}
