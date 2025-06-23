using FrontEASE.Domain.Services.Core.Connector;

namespace FrontEASE.Domain.Services.Core
{
    public class CoreService : ICoreService
    {
        private readonly ICoreConnector _coreConnector;

        public CoreService(ICoreConnector coreConnector)
        {
            _coreConnector = coreConnector;
        }

        public async Task ImportCoreModule(Entities.Shared.Files.File fileModule, CancellationToken cancellationToken) => await _coreConnector.ImportModule(fileModule, cancellationToken);
        public async Task DeleteCoreModule(string name, CancellationToken cancellationToken) => await _coreConnector.DeleteModule(name, cancellationToken);
        public async Task UpdateCoreModels(CancellationToken cancellationToken) => await _coreConnector.UpdateModels(cancellationToken);
    }
}
