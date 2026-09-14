namespace FrontEASE.Domain.Services.Core
{
    public interface ICoreService
    {
        Task ImportCoreModule(Entities.Shared.Files.File fileModule, CancellationToken cancellationToken);
        Task DeleteCoreModule(string name, CancellationToken cancellationToken);
        Task<string> ReadCoreModule(string name, CancellationToken cancellationToken);
        Task UpdateCoreModule(string name, string content, CancellationToken cancellationToken);
        Task UpdateCoreModels(CancellationToken cancellationToken);
        Task<string> GetAvailableModels(CancellationToken cancellationToken);
        Task SaveAvailableModels(string modelsJson, CancellationToken cancellationToken);
    }
}
