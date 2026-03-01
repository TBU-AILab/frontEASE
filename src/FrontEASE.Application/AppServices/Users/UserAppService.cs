using AutoMapper;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Infrastructure.Exceptions.Enums;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Infrastructure.Utils.Users;
using FrontEASE.Domain.Services.Shared.Images;
using FrontEASE.Domain.Services.Users;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.Enums.Auth;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Infrastructure.Constants.UI.Specific;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace FrontEASE.Application.AppServices.Users
{
    public class UserAppService(
        IUserService userService,
        IImageService imageService,
        IMapper mapper,
        IHttpContextAccessor contextAccessor,
        IUserHelper userHelper) : IUserAppService
    {
        private void CheckPermissionsToDelete(ApplicationUser? currentUser, ApplicationUser? deletedUser)
        {
            /* deleted not found */
            if (deletedUser is null)
            { throw new NotFoundException(); }

            var currentRole = userHelper.UserRoleGuidToRole(Guid.Parse(currentUser?.UserRole!.RoleId ?? Guid.Empty.ToString()));
            var deletedRole = deletedUser is not null
                ? userHelper.UserRoleGuidToRole(Guid.Parse(deletedUser.UserRole!.RoleId))
                : UserRole.USER;

            if (
                currentUser is null ||                                      // anonymous cannot delete
                currentUser.Id == deletedUser!.Id ||                        // cannot delete self
                (currentRole != UserRole.OWNER &&                           // non-OWNER ...
                (int)deletedRole > (int)currentRole)                        // ... cannot delete higher role
               )
            {
                throw new UnauthorizedException();
            }
        }

        private void CheckPermissionToCreate(ApplicationUser? currentUser, UserRole insertedRole)
        {
            var currentRole = userHelper.UserRoleGuidToRole(Guid.Parse(currentUser?.UserRole!.RoleId ?? Guid.Empty.ToString()));
            if (
                currentUser is null ||                                  // anonymous cannot create
                (currentRole != UserRole.OWNER &&                       // non-OWNER ...
                 (int)insertedRole > (int)currentRole)                  // ... cannot create higher role
               )
            {
                throw new UnauthorizedException();
            }
        }

        private void CheckPermissionToUpdate(ApplicationUser? currentUser, ApplicationUser? updatedUser, UserRole newTargetRole)
        {
            /* updated not found */
            if (updatedUser is null)
            { throw new NotFoundException(); }

            var currentRole = userHelper.UserRoleGuidToRole(Guid.Parse(currentUser?.UserRole!.RoleId ?? Guid.Empty.ToString()));
            var originalTargetRole = userHelper.UserRoleGuidToRole(Guid.Parse(updatedUser.UserRole!.RoleId));

            if (
                currentUser is null ||                                                  // anonymous cannot update
                (currentRole != UserRole.OWNER &&                                       // non-OWNER ...
                 (
                     (currentUser.Id == updatedUser.Id &&                               // ... cannot change own role
                      newTargetRole != originalTargetRole) ||
                     (int)originalTargetRole > (int)currentRole ||                      // ... cannot modify higher role user
                     (int)newTargetRole > (int)currentRole                              // ... cannot assign higher role
                 ))
               )
            {
                throw new UnauthorizedException();
            }
        }

        public async Task<ApplicationUserDto> Load(CancellationToken cancellationToken)
        {
            var userID = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await userService.Load(Guid.Parse(userID), cancellationToken);

            var userDto = mapper.Map<ApplicationUserDto>(user);
            userDto.Role = userHelper.UserRoleGuidToRole(Guid.Parse(user!.UserRole!.RoleId));
            return userDto;
        }

        public async Task<IList<ApplicationUserDto>> LoadAll(CancellationToken cancellationToken)
        {
            var userEntities = await userService.LoadAll(cancellationToken);
            var userDtos = mapper.Map<IList<ApplicationUserDto>>(userEntities);
            foreach (var userEntity in userEntities)
            {
                var userDto = userDtos.Single(x => x.Id.ToString() == userEntity.Id);
                userDto.Role = userHelper.UserRoleGuidToRole(Guid.Parse(userEntity.UserRole!.RoleId));
                userDto.Image ??= new();
            }
            return userDtos;
        }

        public async Task<ApplicationUserDto> Create(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            var currentUser = await userService.LoadCurrentUser(cancellationToken);
            CheckPermissionToCreate(currentUser, user.Role);

            var userEntity = mapper.Map<ApplicationUser>(user);
            var duplicities = await userService.LoadDuplicities(userEntity, cancellationToken);
            if (duplicities.Count > 0)
            {
                throw new BadRequestException()
                {
                    InternalExceptionCode = ApiInternalExceptionCode.BAD_REQUEST,
                    Errors = new Dictionary<string, string[]>()
                    {{nameof(user),new string[] { $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.BadRequest}.{nameof(ApplicationUser)}.{UIExceptionConstants.UserExists}"}}}
                };
            }

            var insertedEntity = await userService.Create(userEntity, user.Role, user.Password!, cancellationToken);
            var insertedDto = mapper.Map<ApplicationUserDto>(insertedEntity);
            insertedDto.Role = userHelper.UserRoleGuidToRole(Guid.Parse(insertedEntity.UserRole!.RoleId));

            return insertedDto;
        }

        public async Task<ApplicationUserDto> Update(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            var currentUser = await userService.LoadCurrentUser(cancellationToken);
            var editedUserEntity = await userService.Load(user.Id!.Value, cancellationToken);
            CheckPermissionToUpdate(currentUser, editedUserEntity, user.Role);

            var userEntity = mapper.Map<ApplicationUser>(user);
            var newDuplicities = await userService.LoadDuplicities(userEntity, cancellationToken);
            if (newDuplicities.Count > 0)
            {
                var isDuplicityOnlyWithSelf = newDuplicities.Count == 1 && newDuplicities.First().Id == userEntity.Id;
                if (!isDuplicityOnlyWithSelf)
                {
                    throw new BadRequestException()
                    {
                        InternalExceptionCode = ApiInternalExceptionCode.BAD_REQUEST,
                        Errors = new Dictionary<string, string[]>()
                        {{nameof(user),new string[] { $"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.BadRequest}.{nameof(ApplicationUser)}.{UIExceptionConstants.UserExists}"}}}
                    };
                }
            }

            if (!string.IsNullOrWhiteSpace(user.Image?.ImageData) && !string.IsNullOrWhiteSpace(editedUserEntity!.Image?.ImageUrl))
            { imageService.DeleteImage(editedUserEntity.Image); }

            mapper.Map(userEntity, editedUserEntity);
            editedUserEntity = await userService.Update(editedUserEntity!, user.Role, cancellationToken);

            var editedUserDto = mapper.Map<ApplicationUserDto>(editedUserEntity);
            editedUserDto.Role = userHelper.UserRoleGuidToRole(Guid.Parse(editedUserEntity.UserRole!.RoleId));
            editedUserDto.Image ??= new();

            return editedUserDto;
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var currentUser = await userService.LoadCurrentUser(cancellationToken);
            var deletedEntity = await userService.Load(id, cancellationToken);
            CheckPermissionsToDelete(currentUser, deletedEntity);

            await userService.Delete(deletedEntity!, cancellationToken);
        }
    }
}
