using AutoMapper;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Repositories.Management;

namespace FrontEASE.Domain.Services.Management
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
