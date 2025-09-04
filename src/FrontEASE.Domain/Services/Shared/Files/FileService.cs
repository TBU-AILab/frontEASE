using FrontEASE.Domain.DataQueries.Tasks;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Tasks;
using FrontEASE.Domain.Services.Core.Connector;
using FrontEASE.Shared.Data.Enums.Shared.Files;
using Microsoft.AspNetCore.Mvc;

namespace FrontEASE.Domain.Services.Shared.Files
{
    public class FileService : IFileService
    {
        private readonly ICoreConnector _coreService;
        private readonly ITaskRepository _taskRepository;

        public FileService(ITaskRepository taskRepository, ICoreConnector coreService)
        {
            _taskRepository = taskRepository;
            _coreService = coreService;
        }

        public async Task<(FileStreamResult? Stream, string? Name)?> GetArchive(Guid identifier, FileSpecification type, CancellationToken cancellationToken)
        {
            return type switch
            {
                FileSpecification.TASK_SOLUTION_ARCHIVE => ((FileStreamResult? Stream, string? Name)?)await GetTaskSolutionArchive(identifier, cancellationToken),
                FileSpecification.TASK_FULL_ARCHIVE => ((FileStreamResult? Stream, string? Name)?)await GetTaskFullArchive(identifier, cancellationToken),
                _ => null,
            };
        }

        private async Task<(FileStreamResult? Stream, string? Name)> GetTaskSolutionArchive(Guid identifier, CancellationToken cancellationToken)
        {
            var query = new TasksQuery()
            {
                LoadSolutions = true,
                WithNoTracking = true,
            };

            var task = (await _taskRepository.LoadAllWhere(x => x.Solutions.Any(x => x.ID == identifier), query, cancellationToken)).SingleOrDefault() ?? throw new NotFoundException();
            var solution = task.Solutions.Single(x => x.ID == identifier);
            var result = await _coreService.DownloadTaskSolution(task.ID, solution.TaskMessageID, cancellationToken);

            var name = $"{task.Config.Name}_{solution.ID}";
            return (result, name);
        }

        private async Task<(FileStreamResult? Stream, string Name)> GetTaskFullArchive(Guid identifier, CancellationToken cancellationToken)
        {
            var query = new TasksQuery()
            {
                LoadConfig = true,
                WithNoTracking = true,
            };

            var task = await _taskRepository.Load(identifier, query, cancellationToken) ?? throw new NotFoundException();
            var result = await _coreService.DownloadTaskFull(task.ID, cancellationToken);

            var name = task.Config.Name;
            return (result, name);
        }
    }
}
