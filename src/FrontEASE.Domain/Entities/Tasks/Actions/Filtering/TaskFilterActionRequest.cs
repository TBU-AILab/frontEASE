using FrontEASE.Shared.Data.Enums.Tasks;

namespace FrontEASE.Domain.Entities.Tasks.Actions.Filtering
{
    public class TaskFilterActionRequest
    {
        public TaskFilterActionRequest()
        {
            State = [];
        }

        public string? Name { get; set; }
        public string? MessagesContent { get; set; }
        public IList<TaskState> State { get; set; }
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public DateTime? DateUpdatedFrom { get; set; }
        public DateTime? DateUpdatedTo { get; set; }
    }
}
