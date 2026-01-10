using Blazorise.Charts;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Messages;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;

namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks.Charts
{
    public interface ITaskChartManipulationService
    {
        (IList<float> Values, IList<string> Labels) GetValueEvolutionChartData(IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions);
        (IList<float> Values, IList<string> Labels) GetValueConvergenceChartData(IList<TaskMessageDto> messages, IList<TaskSolutionDto> solutions);
        LineChartOptions PrepareLineChartOptions(string titleText, string subtitleText);
    }
}
