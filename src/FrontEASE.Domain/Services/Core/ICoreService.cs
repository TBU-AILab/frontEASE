namespace FrontEASE.Domain.Services.Core
{
    public interface ICoreService
    {
        Task ImportCoreModule(Entities.Shared.Files.File fileModule, CancellationToken cancellationToken);
        Task DeleteCoreModule(string name, CancellationToken cancellationToken);
        Task UpdateCoreModels(CancellationToken cancellationToken);
    }
}
