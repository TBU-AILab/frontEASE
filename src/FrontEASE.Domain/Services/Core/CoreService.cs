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

        public async Task ImportCoreModule(Entities.Shared.Files.File fileModule) =>
            await _coreConnector.HandleModuleImport(fileModule);
    }
}
