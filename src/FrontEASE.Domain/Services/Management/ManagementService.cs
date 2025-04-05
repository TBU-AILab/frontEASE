using AutoMapper;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Core.Packages;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Management;
using FrontEASE.Domain.Services.Core;

namespace FrontEASE.Domain.Services.Management
{
    public class ManagementService : IManagementService
    {
        private readonly IManagementRepository _managementRepository;
        private readonly IEASECoreService _coreService;
        private readonly IMapper _mapper;

        public ManagementService(
            IManagementRepository managementRepository,
            IEASECoreService coreService,
            IMapper mapper)
        {
            _coreService = coreService;
            _managementRepository = managementRepository;
            _mapper = mapper;
        }

        public async Task<GlobalPreferences> LoadGlobal()
        {
            var packages = await _coreService.GetPackageTypes();
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
            return globalPrefs!;
        }
    }
}
