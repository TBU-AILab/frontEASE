using AutoMapper;
using FrontEASE.Domain.DataQueries.Management;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Core.Packages;
using FrontEASE.Domain.Entities.Management.Tags;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Management;
using FrontEASE.Domain.Services.Core.Connector;
using Microsoft.EntityFrameworkCore;

namespace FrontEASE.Domain.Services.Management
{
    public class ManagementService(
        IManagementRepository managementRepository,
        ICoreConnector coreService,
        IMapper mapper) : IManagementService
    {
        public async Task<UserPreferenceTagOption?> InsertTag(UserPreferenceTagOption tag, CancellationToken cancellationToken)
        {
            var tagEntity = await managementRepository.InsertTag(tag, cancellationToken);
            return tagEntity;
        }

        public async Task<UserPreferenceTagOption?> LoadTag(string tag, bool suppressException, CancellationToken cancellationToken)
        {
            var tagEntity = await managementRepository.LoadTagWhere(x => EF.Functions.ILike(x.Tag, tag), cancellationToken);
            if (tagEntity is null && !suppressException)
            {
                throw new NotFoundException();
            }
            return tagEntity;
        }

        public async Task<UserPreferenceTagOption?> LoadTag(Guid id, bool suppressException, CancellationToken cancellationToken)
        {
            var tagEntity = await managementRepository.LoadTag(id, cancellationToken);
            if (tagEntity is null && !suppressException)
            {
                throw new NotFoundException();
            }
            return tagEntity;
        }

        public async Task<IList<UserPreferenceTagOption>> LoadTags(CancellationToken cancellationToken)
        {
            var tags = await managementRepository.LoadTags(cancellationToken);
            return tags.Any() ? tags : throw new NotFoundException();
        }

        public async Task<GlobalPreferences> LoadGlobal(CancellationToken cancellationToken)
        {
            var packages = await coreService.GetPackages(cancellationToken);
            var globalPrefs = new GlobalPreferences()
            {
                CorePackages = mapper.Map<IList<GlobalPreferenceCorePackage>>(packages)
            };

            return globalPrefs;
        }

        public async Task<UserPreferences> LoadBase(Guid id, CancellationToken cancellationToken)
        {
            var query = new UserPreferencesQuery();
            var preferences = await managementRepository.Load(id, query, cancellationToken) ?? throw new NotFoundException();
            return preferences;
        }

        public async Task<UserPreferences> LoadFull(Guid id, CancellationToken cancellationToken)
        {
            var query = new UserPreferencesQuery
            {
                LoadTokenOptions = true,
                LoadConnectorTypes = true,
                LoadGeneralOptions = true,
                LoadTagOptions = true,
                WithNoTracking = true,
                AsSplitQuery = true
            };

            var preferences = await managementRepository.Load(id, query, cancellationToken) ?? throw new NotFoundException();
            return preferences;
        }

        public async Task<UserPreferences> Update(Guid id, UserPreferences preferences, CancellationToken cancellationToken)
        {
            var query = new UserPreferencesQuery
            {
                LoadTokenOptions = true,
                LoadConnectorTypes = true,
                LoadGeneralOptions = true,
                AsSplitQuery = true
            };

            var updated = await managementRepository.Load(id, query, cancellationToken) ?? throw new NotFoundException();
            mapper.Map(preferences, updated);
            HandleDependentEntities(updated);

            updated = await managementRepository.Update(updated, cancellationToken);
            return updated!;
        }

        public async Task<GlobalPreferences> UpdateGlobal(GlobalPreferences preferences, CancellationToken cancellationToken)
        {
            var globalPrefs = await LoadGlobal(cancellationToken);
            await HandleCorePackages(globalPrefs.CorePackages, preferences.CorePackages, cancellationToken);

            var updatedPrefs = await LoadGlobal(cancellationToken);
            return updatedPrefs!;
        }

        public async Task DeleteTag(UserPreferenceTagOption tag, CancellationToken cancellationToken) =>
            await managementRepository.Delete(tag, cancellationToken);

        private async Task HandleCorePackages(IList<GlobalPreferenceCorePackage> origPackageConfig, IList<GlobalPreferenceCorePackage> newPackageConfig, CancellationToken cancellationToken)
        {
            var packagesToDelete = origPackageConfig
                .Where(orig => !orig.System && !newPackageConfig.Any(newPkg => newPkg.Name == orig.Name && newPkg.Version == orig.Version))
                .ToList();

            var packagesToInstall = newPackageConfig
                .Where(newPkg => !newPkg.System && !origPackageConfig.Any(orig => orig.Name == newPkg.Name && orig.Version == newPkg.Version))
                .ToList();

            if (packagesToDelete.Count > 0) { await coreService.DeletePackages(packagesToDelete, cancellationToken); }
            if (packagesToInstall.Count > 0) { await coreService.AddPackages(packagesToInstall, cancellationToken); }
        }

        private static void HandleDependentEntities(UserPreferences preferences)
        {
            preferences.TagOptions.Clear();
        }
    }
}
