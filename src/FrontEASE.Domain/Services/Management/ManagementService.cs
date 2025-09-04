using AutoMapper;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Core.Packages;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Management;
using FrontEASE.Domain.Services.Core.Connector;

namespace FrontEASE.Domain.Services.Management
{
    public class ManagementService : IManagementService
    {
        private readonly IManagementRepository _managementRepository;
        private readonly ICoreConnector _coreService;
        private readonly IMapper _mapper;

        public ManagementService(
            IManagementRepository managementRepository,
            ICoreConnector coreService,
            IMapper mapper)
        {
            _coreService = coreService;
            _managementRepository = managementRepository;
            _mapper = mapper;
        }

        public async Task<GlobalPreferences> LoadGlobal(CancellationToken cancellationToken)
        {
            var packages = await _coreService.GetPackages(cancellationToken);
            var globalPrefs = new GlobalPreferences()
            {
                CorePackages = _mapper.Map<IList<GlobalPreferenceCorePackage>>(packages)
            };

            return globalPrefs;
        }

        public async Task<UserPreferences> Load(Guid id, CancellationToken cancellationToken)
        {
            var preferences = await _managementRepository.Load(id, cancellationToken) ?? throw new NotFoundException();
            return preferences;
        }

        public async Task<UserPreferences> Update(Guid id, UserPreferences preferences, CancellationToken cancellationToken)
        {
            var updated = await Load(id, cancellationToken);
            _mapper.Map(preferences, updated);

            updated = await _managementRepository.Update(updated, cancellationToken);
            return updated!;
        }

        public async Task<GlobalPreferences> UpdateGlobal(GlobalPreferences preferences, CancellationToken cancellationToken)
        {
            var globalPrefs = await LoadGlobal(cancellationToken);
            await HandleCorePackages(globalPrefs.CorePackages, preferences.CorePackages, cancellationToken);

            var updatedPrefs = await LoadGlobal(cancellationToken);
            return updatedPrefs!;
        }

        private async Task HandleCorePackages(IList<GlobalPreferenceCorePackage> origPackageConfig, IList<GlobalPreferenceCorePackage> newPackageConfig, CancellationToken cancellationToken)
        {
            var packagesToDelete = origPackageConfig
                .Where(orig => !orig.System && !newPackageConfig.Any(newPkg => newPkg.Name == orig.Name && newPkg.Version == orig.Version))
                .ToList();

            var packagesToInstall = newPackageConfig
                .Where(newPkg => !newPkg.System && !origPackageConfig.Any(orig => orig.Name == newPkg.Name && orig.Version == newPkg.Version))
                .ToList();

            if (packagesToDelete.Count > 0) { await _coreService.DeletePackages(packagesToDelete, cancellationToken); }
            if (packagesToInstall.Count > 0) { await _coreService.AddPackages(packagesToInstall, cancellationToken); }
        }
    }
}
