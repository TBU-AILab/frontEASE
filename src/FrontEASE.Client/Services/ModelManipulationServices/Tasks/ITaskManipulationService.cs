using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;
using FrontEASE.Shared.Data.Enums.Tasks.Config;

namespace FrontEASE.Client.Services.ModelManipulationServices.Tasks
{
    public interface ITaskManipulationService
    {
        void AddTaskRepeatedMessageItem(TaskDto task);
        void RemoveTaskRepeatedMessageItem(TaskDto task, TaskConfigRepeatedMessageItemDto item);

        void AddModule(IList<TaskModuleDto> modules, ModuleType moduleType);
        void RemoveModule(IList<TaskModuleDto> modules, TaskModuleDto item);
        void SwapSelectedModule(TaskModuleNoValidationDto? source, TaskModuleDto destination);

        bool UpdateTaskStatuses(IList<TaskInfoDto> tasks, IList<TaskStatusDto> taskStatuses);

        void CleanUsersInfo(TaskDto task);
        void CleanCompaniesInfo(TaskDto task);

        void PrepareTaskRequest(TaskDto task, bool cleanImages, bool cleanOptions);
        void PrepareTaskFilter(TaskFilterActionRequestDto filter);
    }
}
