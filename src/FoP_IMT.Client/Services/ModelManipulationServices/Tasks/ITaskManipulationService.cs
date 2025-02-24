using FoP_IMT.Shared.Data.DTOs.Tasks;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;
using FoP_IMT.Shared.Data.DTOs.Tasks.UI;
using FoP_IMT.Shared.Data.Enums.Tasks.Config;

namespace FoP_IMT.Client.Services.ModelManipulationServices.Tasks
{
    public interface ITaskManipulationService
    {
        void AddTaskRepeatedMessageItem(TaskDto task);
        void RemoveTaskRepeatedMessageItem(TaskDto task, TaskConfigRepeatedMessageItemDto item);

        void AddModule(IList<TaskModuleDto> modules, ModuleType moduleType);
        void RemoveModule(IList<TaskModuleDto> modules, TaskModuleDto item);
        void SwapSelectedModule(TaskModuleNoValidationDto? source, TaskModuleDto destination);

        void UpdateTaskStatuses(IList<TaskInfoDto> tasks, IList<TaskStatusDto> taskStatuses);

        void CleanUsersInfo(TaskDto task);
        void CleanCompaniesInfo(TaskDto task);

        void PrepareTaskRequest(TaskDto task, bool cleanImages, bool cleanOptions);
    }
}
