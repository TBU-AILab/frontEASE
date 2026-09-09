using FrontEASE.Shared.Data.DTOs.Tasks.Results;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Actions.Results
{
    /// <summary>
    /// Updated task summaries returned after a successful bulk edit.
    /// </summary>
    public class TaskBulkEditResultDto : ITaskOperationResultDto
    {
        public TaskBulkEditResultDto()
        {
            Tasks = [];
        }

        public IList<TaskInfoDto> Tasks { get; set; }
    }
}
