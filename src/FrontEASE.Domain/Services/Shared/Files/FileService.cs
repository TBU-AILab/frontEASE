using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Services.Core;
using FrontEASE.Shared.Data.Enums.Shared.Files;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Domain.Services.Shared.Files
{
    public class FileService : IFileService
    {
        private readonly IEASECoreService _coreService;
        private readonly ITaskRepository _taskRepository;

        public FileService(ITaskRepository taskRepository, IEASECoreService coreService)
        {
            _taskRepository = taskRepository;
            _coreService = coreService;
        }

        public async Task<(FileStreamResult? Stream, string? Name)?> GetArchive(Guid identifier, FileSpecification type)
        {
            return type switch
            {
                FileSpecification.TASK_SOLUTION_ARCHIVE => ((FileStreamResult? Stream, string? Name)?)await GetTaskSolutionArchive(identifier),
                FileSpecification.TASK_FULL_ARCHIVE => ((FileStreamResult? Stream, string? Name)?)await GetTaskFullArchive(identifier),
                _ => null,
            };
        }

        private async Task<(FileStreamResult? Stream, string? Name)> GetTaskSolutionArchive(Guid identifier)
        {
            var query = new TasksQuery()
            {
                LoadSolutions = true,
                WithNoTracking = true,
            };

            var task = (await _taskRepository.LoadAllWhere(x => x.Solutions.Any(x => x.ID == identifier), query)).SingleOrDefault() ?? throw new NotFoundException();
            var solution = task.Solutions.Single(x => x.ID == identifier);
            var result = await _coreService.DownloadTaskSolution(task.ID, solution.TaskMessageID);

            var name = $"{task.Config.Name}_{solution.ID}";
            return (result, name);
        }

        private async Task<(FileStreamResult? Stream, string Name)> GetTaskFullArchive(Guid identifier)
        {
            var query = new TasksQuery()
            {
                LoadConfig = true,
                WithNoTracking = true,
            };

            var task = await _taskRepository.Load(identifier, query) ?? throw new NotFoundException();
            var result = await _coreService.DownloadTaskFull(task.ID);

            var name = task.Config.Name;
            return (result, name);
        }
    }
}
