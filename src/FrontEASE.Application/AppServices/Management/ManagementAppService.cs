using AutoMapper;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Services.Management;
using FrontEASE.Domain.Services.Users;
using FrontEASE.Shared.Data.DTOs.Management;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FrontEASE.Application.AppServices.Management
{
    public class ManagementAppService : IManagementAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IManagementService _managementService;
        public ManagementAppService(
            IMapper mapper,
            IUserService userService,
            IManagementService managementService,
            IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _userService = userService;
            _contextAccessor = contextAccessor;
            _managementService = managementService;
        }

        public async Task<UserPreferencesDto> Load(CancellationToken cancellationToken)
        {
            var id = await GetUserID(cancellationToken);

            var preferencesEntity = await _managementService.Load(id, cancellationToken);
            var preferencesDto = _mapper.Map<UserPreferencesDto>(preferencesEntity);
            return preferencesDto;
        }

        public async Task<GlobalPreferencesDto> LoadGlobal(CancellationToken cancellationToken)
        {
            var globalPreferences = await _managementService.LoadGlobal(cancellationToken);
            var preferencesDto = _mapper.Map<GlobalPreferencesDto>(globalPreferences);
            return preferencesDto;
        }

        public async Task<UserPreferencesDto> Update(UserPreferencesDto preferences, CancellationToken cancellationToken)
        {
            var id = await GetUserID(cancellationToken);
            var preferencesEntity = _mapper.Map<UserPreferences>(preferences);

            var updated = await _managementService.Update(id, preferencesEntity, cancellationToken);
            var updatedDto = _mapper.Map<UserPreferencesDto>(updated);
            return updatedDto;
        }

        public async Task<GlobalPreferencesDto> UpdateGlobal(GlobalPreferencesDto globalPreferences, CancellationToken cancellationToken)
        {
            var preferencesEntity = _mapper.Map<GlobalPreferences>(globalPreferences);

            var updated = await _managementService.UpdateGlobal(preferencesEntity, cancellationToken);
            var updatedDto = _mapper.Map<GlobalPreferencesDto>(updated);
            return updatedDto;
        }

        private async Task<Guid> GetUserID(CancellationToken cancellationToken)
        {
            var userMail = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userService.Load(userMail, cancellationToken);

            return Guid.Parse(user!.Id);
        }
    }
}
