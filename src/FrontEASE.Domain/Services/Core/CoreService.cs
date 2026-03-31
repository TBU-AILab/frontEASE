using FrontEASE.Domain.Services.Core.Connector;

namespace FrontEASE.Domain.Services.Core
{
    public class CoreService(ICoreConnector coreConnector) : ICoreService
    {
        public async Task ImportCoreModule(Entities.Shared.Files.File fileModule, CancellationToken cancellationToken) => await coreConnector.ImportModule(fileModule, cancellationToken);
        public async Task DeleteCoreModule(string name, CancellationToken cancellationToken) => await coreConnector.DeleteModule(name, cancellationToken);
        public async Task UpdateCoreModels(CancellationToken cancellationToken) => await coreConnector.UpdateModels(cancellationToken);
        public async Task<string> GetAvailableModels(CancellationToken cancellationToken) => await coreConnector.GetAvailableModels(cancellationToken);
        public async Task SaveAvailableModels(string modelsJson, CancellationToken cancellationToken) => await coreConnector.SaveAvailableModels(modelsJson, cancellationToken);
    }
}
