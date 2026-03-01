using AutoMapper;
using FrontEASE.Domain.Entities.Management;
using FrontEASE.Domain.Entities.Management.Tags;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Infrastructure.Exceptions.Enums;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Infrastructure.Utils.Users;
using FrontEASE.Domain.Services.Management;
using FrontEASE.Domain.Services.Users;
using FrontEASE.Shared.Data.DTOs.Management;
using FrontEASE.Shared.Data.DTOs.Management.Tags;
using FrontEASE.Shared.Data.Enums.Auth;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Infrastructure.Constants.UI.Specific;
using System.Net;

namespace FrontEASE.Application.AppServices.Management
{
    public class ManagementAppService(
        IMapper mapper,
        IUserService userService,
        IManagementService managementService,
        IUserHelper userHelper) : IManagementAppService
    {

        private void CheckPermissionsToDelete(ApplicationUser? currentUser, UserPreferenceTagOption? deletedTag)
        {
            /* deleted not found */
            if (deletedTag is null)
            { throw new NotFoundException(); }

            if (currentUser is null)
            { throw new UnauthorizedException(); }

            var currentRole = userHelper.UserRoleGuidToRole(Guid.Parse(currentUser.UserRole!.RoleId ?? Guid.Empty.ToString()));

            /* OWNER can always delete any tag */
            if (currentRole == UserRole.OWNER)
            { return; }

            var deletedTagOwner = deletedTag.UserPreferences.User;
            var isAuthor = currentUser.Id == deletedTagOwner.Id;
            var hasLinkedTasks = deletedTag.Tasks.Count > 0;

            /* others can delete a tag even if they own it and it does not have any linked tasks */
            if (!isAuthor || hasLinkedTasks)
            {
                throw new UnauthorizedException();
            }
        }

        public async Task<UserPreferenceTagOptionDto> Create(UserPreferenceTagOptionDto tag, CancellationToken cancellationToken)
        {
            var duplicate = await managementService.LoadTag(tag.Tag, true, cancellationToken);
            if (duplicate is not null)
            {
                throw new BadRequestException()
                {
                    InternalExceptionCode = ApiInternalExceptionCode.BAD_REQUEST,
                    Errors = new Dictionary<string, string[]>()
                    {{nameof(tag),new string[] { $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.BadRequest}.{nameof(UserPreferenceTagOptionDto)}.{UIExceptionConstants.TagExists}"}}}
                };
            }

            var id = await userService.LoadCurrentUserId(cancellationToken);
            var preferencesEntity = await managementService.LoadBase(id, cancellationToken);
            var insertedEntity = mapper.Map<UserPreferenceTagOption>(tag);

            insertedEntity.UserPreferences = preferencesEntity;

            await managementService.InsertTag(insertedEntity, cancellationToken);
            var insertedDto = mapper.Map<UserPreferenceTagOptionDto>(insertedEntity);
            return insertedDto;
        }

        public async Task<IList<UserPreferenceTagOptionDto>> LoadTags(CancellationToken cancellationToken)
        {
            var tagsEntityList = await managementService.LoadTags(cancellationToken);
            var tagsDtoList = mapper.Map<IList<UserPreferenceTagOptionDto>>(tagsEntityList);
            return tagsDtoList;
        }

        public async Task<UserPreferencesDto> Load(CancellationToken cancellationToken)
        {
            var id = await userService.LoadCurrentUserId(cancellationToken);

            var preferencesEntity = await managementService.LoadFull(id, cancellationToken);
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
            var id = await userService.LoadCurrentUserId(cancellationToken);
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

        public async Task DeleteTag(Guid id, CancellationToken cancellationToken)
        {
            var currentUser = await userService.LoadCurrentUser(cancellationToken);
            var deletedEntity = await managementService.LoadTag(id, false, cancellationToken);

            CheckPermissionsToDelete(currentUser, deletedEntity);

            await managementService.DeleteTag(deletedEntity!, cancellationToken);
        }
    }
}
