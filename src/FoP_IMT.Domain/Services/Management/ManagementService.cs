using AutoMapper;
using FoP_IMT.Domain.Entities.Management;
using FoP_IMT.Domain.Infrastructure.Exceptions.Types;
using FoP_IMT.Domain.Repositories.Management;

namespace FoP_IMT.Domain.Services.Management
{
    public class ManagementService : IManagementService
    {
        private readonly IManagementRepository _managementRepository;
        private readonly IMapper _mapper;

        public ManagementService(
            IManagementRepository managementRepository,
            IMapper mapper)
        {
            _managementRepository = managementRepository;
            _mapper = mapper;
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
    }
}
