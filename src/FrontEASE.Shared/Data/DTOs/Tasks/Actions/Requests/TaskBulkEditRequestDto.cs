using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs;

namespace FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests
{
    /// <summary>
    /// Applies selected values from a configuration template to multiple tasks.
    /// </summary>
    public class TaskBulkEditRequestDto
    {
        public TaskBulkEditRequestDto()
        {
            TaskIDs = [];
            Template = new();
            Fields = new();
        }

        public IList<Guid> TaskIDs { get; set; }
        public TaskConfigDto Template { get; set; }
        public TaskBulkEditFieldsDto Fields { get; set; }
    }
}
