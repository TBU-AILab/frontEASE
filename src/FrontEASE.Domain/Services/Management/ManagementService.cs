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

        public async Task<GlobalPreferences> LoadGlobal()
        {
            var packages = await _coreService.GetPackages();
            var globalPrefs = new GlobalPreferences()
            {
                CorePackages = _mapper.Map<IList<GlobalPreferenceCorePackage>>(packages)
            };

            return globalPrefs;
        }

        public async Task<UserPreferences> Load(Guid id)
        {
            var preferences = await _managementRepository.Load(id) ?? throw new NotFoundException();
            return preferences;
        }

        public async Task<UserPreferences> Update(Guid id, UserPreferences preferences)
        {
            var updated = await Load(id);
            _mapper.Map(preferences, updated);

            updated = await _managementRepository.Update(updated);
            return updated!;
        }

        public async Task<GlobalPreferences> UpdateGlobal(GlobalPreferences preferences)
        {
            var globalPrefs = await LoadGlobal();
            await HandleCorePackages(globalPrefs.CorePackages, preferences.CorePackages);

            var updatedPrefs = await LoadGlobal();
            return updatedPrefs!;
        }

        private async Task HandleCorePackages(IList<GlobalPreferenceCorePackage> origPackageConfig, IList<GlobalPreferenceCorePackage> newPackageConfig)
        {
            var packagesToDelete = origPackageConfig
                .Where(orig => !orig.System && !newPackageConfig.Any(newPkg => newPkg.Name == orig.Name && newPkg.Version == orig.Version))
                .ToList();

            var packagesToInstall = newPackageConfig
                .Where(newPkg => !newPkg.System && !origPackageConfig.Any(orig => orig.Name == newPkg.Name && orig.Version == newPkg.Version))
                .ToList();

            if (packagesToDelete.Count > 0) { await _coreService.DeletePackages(packagesToDelete); }
            if (packagesToInstall.Count > 0) { await _coreService.AddPackages(packagesToInstall); }
        }
    }
}
