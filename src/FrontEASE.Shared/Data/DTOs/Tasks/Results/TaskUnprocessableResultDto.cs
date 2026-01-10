using FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Results
{
    /// <summary>
    /// Model transporting information about incorrectly performed API call in tasks section - referring to unprocessable entity
    /// </summary>
    public class TaskUnprocessableResultDto : UnprocessableResultDto, ITaskOperationResultDto
    {
    }
}
