using FrontEASE.Domain.Services.Core.Connector;

namespace FrontEASE.Domain.Services.Core
{
    public class CoreService(ICoreConnector coreConnector) : ICoreService
    {
        public async Task ImportCoreModule(Entities.Shared.Files.File fileModule, CancellationToken cancellationToken) => await coreConnector.ImportModule(fileModule, cancellationToken);
        public async Task DeleteCoreModule(string name, CancellationToken cancellationToken) => await coreConnector.DeleteModule(name, cancellationToken);
        public async Task UpdateCoreModels(CancellationToken cancellationToken) => await coreConnector.UpdateModels(cancellationToken);
    }
}
