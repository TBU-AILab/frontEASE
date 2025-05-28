namespace FrontEASE.Domain.Services.Core
{
    public interface ICoreService
    {
        Task ImportCoreModule(Entities.Shared.Files.File fileModule);
        Task DeleteCoreModule(string name);
        Task UpdateCoreModels();
    }
}
