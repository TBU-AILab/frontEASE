using ApexCharts;
using FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts.Models;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;

namespace FrontEASE.Client.Services.HelperServices.UI.Charts
{
    public interface IChartHelper
    {
        ApexChartOptions<ChartDataPoint> PrepareLineChartOptions(string titleText, string subtitleText);
        int DetermineDecimalPrecision(IList<TaskSolutionDto> solutions);
    }
}
