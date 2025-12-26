using Blazorise;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Shared.General;
using Microsoft.AspNetCore.Components.Forms;

namespace FrontEASE.Client.Contexts.Specific.Tasks
{
    public class TaskModalContext(
        TaskDto task,
        TaskInfoDto taskInfo,
        TaskViewMetadataDto metadata,
        Modal modal,
        DataOperation operation,
        Validations? validations = null,
        EditContext? editContext = null)
    {
        public TaskDto Task { get; set; } = task;
        public TaskInfoDto TaskInfo { get; set; } = taskInfo;
        public TaskViewMetadataDto TaskMetadata { get; set; } = metadata;
        public Modal Modal { get; set; } = modal;
        public DataOperation Operation { get; set; } = operation;
        public Validations? Validations { get; set; } = validations;
        public EditContext? EditContext { get; set; } = editContext;
    }
}
