using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.RepeatedMessage;
using FoP_IMT.Shared.Data.DTOs.Tasks.UI;
using FoP_IMT.Shared.Data.Enums.Tasks.Config;

namespace FoP_IMT.Client.Services.ModelManipulationServices.Tasks
{
    public class TaskManipulationService : ITaskManipulationService
    {
        private readonly IMapper _mapper;

        public TaskManipulationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddTaskRepeatedMessageItem(TaskDto task) => task.Config.RepeatedMessage.RepeatedMessageItems.Add(new());
        public void RemoveTaskRepeatedMessageItem(TaskDto task, TaskConfigRepeatedMessageItemDto item) => task.Config.RepeatedMessage.RepeatedMessageItems.Remove(item);

        public void AddModule(IList<TaskModuleDto> modules, ModuleType moduleType) => modules.Add(new() { PackageType = moduleType });
        public void RemoveModule(IList<TaskModuleDto> modules, TaskModuleDto item) => modules.Remove(item);

        public void PrepareTaskRequest(TaskDto task, bool cleanImages, bool cleanOptions)
        {
            if (cleanImages)
            {
                foreach (var user in task.Members)
                { user.Image = null; }

                foreach (var group in task.MemberGroups)
                {
                    group.Image = null;
                    foreach (var user in group.Users)
                    { user.Image = null; }
                }
            }
            if (cleanOptions)
            {
                task.Config.AvailableModules.Clear();
            }
        }

        public void SwapSelectedModule(TaskModuleNoValidationDto? source, TaskModuleDto destination)
        {
            if (!string.IsNullOrWhiteSpace(source?.ShortName))
            {
                _mapper.Map(source, destination);
            }
            else
            {
                _mapper.Map(new TaskModuleDto(), destination);
            }
        }

        public void UpdateTaskStatuses(IList<TaskInfoDto> tasks, IList<TaskStatusDto> taskStatuses)
        {
            foreach (var task in tasks)
            {
                var matchingStatus = taskStatuses.FirstOrDefault(status => status.ID == task.ID);
                if (matchingStatus is not null)
                {
                    task.State = matchingStatus.State;
                }
            }
        }

        public void CleanUsersInfo(TaskDto task)
        {
            if (task.Author is not null)
            {
                var author = task.Members.SingleOrDefault(x => x.Id == task.Author.Id);
                task.Members.Clear();

                if (author is not null)
                {
                    task.Members.Add(author);
                }
            }
        }

        public void CleanCompaniesInfo(TaskDto task) => task.MemberGroups.Clear();
    }
}
