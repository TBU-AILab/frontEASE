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

        public async Task ImportCoreModule(Entities.Shared.Files.File fileModule) => await _coreConnector.ImportModule(fileModule);
        public async Task DeleteCoreModule(string name) => await _coreConnector.DeleteModule(name);
        public async Task UpdateCoreModels() => await _coreConnector.UpdateModels();
    }
}
