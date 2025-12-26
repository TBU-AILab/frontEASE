using AutoMapper;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Services.Management;
using FrontEASE.Domain.Services.Users;
using FrontEASE.Shared.Data.DTOs.Management;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FrontEASE.Application.AppServices.Management
{
    public class ManagementAppService(
        IMapper mapper,
        IUserService userService,
        IManagementService managementService,
        IHttpContextAccessor contextAccessor) : IManagementAppService
    {
        public async Task<UserPreferencesDto> Load(CancellationToken cancellationToken)
        {
            var id = await GetUserID(cancellationToken);

            var preferencesEntity = await managementService.Load(id, cancellationToken);
            var preferencesDto = mapper.Map<UserPreferencesDto>(preferencesEntity);
            return preferencesDto;
        }

        public async Task<GlobalPreferencesDto> LoadGlobal(CancellationToken cancellationToken)
        {
            var globalPreferences = await managementService.LoadGlobal(cancellationToken);
            var preferencesDto = mapper.Map<GlobalPreferencesDto>(globalPreferences);
            return preferencesDto;
        }

        public async Task<UserPreferencesDto> Update(UserPreferencesDto preferences, CancellationToken cancellationToken)
        {
            var id = await GetUserID(cancellationToken);
            var preferencesEntity = mapper.Map<UserPreferences>(preferences);

            var updated = await managementService.Update(id, preferencesEntity, cancellationToken);
            var updatedDto = mapper.Map<UserPreferencesDto>(updated);
            return updatedDto;
        }

        public async Task<GlobalPreferencesDto> UpdateGlobal(GlobalPreferencesDto globalPreferences, CancellationToken cancellationToken)
        {
            var preferencesEntity = mapper.Map<GlobalPreferences>(globalPreferences);

            var updated = await managementService.UpdateGlobal(preferencesEntity, cancellationToken);
            var updatedDto = mapper.Map<GlobalPreferencesDto>(updated);
            return updatedDto;
        }

        private async Task<Guid> GetUserID(CancellationToken cancellationToken)
        {
            var userMail = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userService.Load(userMail, cancellationToken);

            return Guid.Parse(user!.Id);
        }
    }
}
