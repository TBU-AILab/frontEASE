using AutoMapper;
using FrontEASE.Domain.Entities.Shared.Users;
using FrontEASE.Domain.Infrastructure.Exceptions.Enums;
using FrontEASE.Domain.Infrastructure.Exceptions.Types;
using FrontEASE.Domain.Infrastructure.Settings.App;
using FrontEASE.Domain.Services.Shared.Images;
using FrontEASE.Domain.Services.Shared.Resources;
using FrontEASE.Domain.Services.Users;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Data.Enums.Auth;
using FrontEASE.Shared.Data.Enums.Shared.Resources;
using FrontEASE.Shared.Infrastructure.Constants.UI.Generic;
using FrontEASE.Shared.Infrastructure.Constants.UI.Specific;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace FrontEASE.Application.AppServices.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly IResourceService _resourceService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppSettings _appSettings;

        public UserAppService(
            IUserService userService,
            IImageService imageService,
            IResourceService resourceService,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            AppSettings appSettings)
        {
            _imageService = imageService;
            _resourceService = resourceService;
            _userService = userService;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _appSettings = appSettings;
        }

        private async Task<ApplicationUser?> LoadCurrentUser(CancellationToken cancellationToken)
        {
            var userMail = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;
            var currentUser = await _userService.Load(userMail, cancellationToken);

            return currentUser;
        }

        private void CheckPermissionsToDelete(ApplicationUser? currentUser, ApplicationUser? deletedUser)
        {
            /* deleted not found */
            if (deletedUser is null)
            { throw new NotFoundException(); }

            var currentRole = UserRoleGuidToRole(Guid.Parse(currentUser?.UserRole!.RoleId ?? Guid.Empty.ToString()));
            var deletedRole = deletedUser is not null
                ? UserRoleGuidToRole(Guid.Parse(deletedUser.UserRole!.RoleId))
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

        private void CheckPermissionToCreate(ApplicationUser? currentUser, ApplicationUser insertedUser)
        {
            var currentRole = UserRoleGuidToRole(Guid.Parse(currentUser?.UserRole!.RoleId ?? Guid.Empty.ToString()));
            var insertedRole = UserRoleGuidToRole(Guid.Parse(insertedUser.UserRole!.RoleId));

            if (
                currentUser is null ||                                  // anonymous cannot create
                (currentRole != UserRole.OWNER &&                       // non-OWNER ...
                 (int)insertedRole > (int)currentRole)                  // ... cannot create higher role
               )
            {
                throw new UnauthorizedException();
            }
        }

        private void CheckPermissionToUpdate(ApplicationUser? currentUser, ApplicationUser? updatedUser, ApplicationUser newUpdatedUser)
        {
            /* updated not found */
            if (updatedUser is null)
            { throw new NotFoundException(); }

            var currentRole = UserRoleGuidToRole(Guid.Parse(currentUser?.UserRole!.RoleId ?? Guid.Empty.ToString()));
            var originalTargetRole = UserRoleGuidToRole(Guid.Parse(updatedUser.UserRole!.RoleId));
            var newTargetRole = UserRoleGuidToRole(Guid.Parse(newUpdatedUser.UserRole!.RoleId));

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
            var userID = _contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await _userService.Load(Guid.Parse(userID), cancellationToken);
            
            var userDto = _mapper.Map<ApplicationUserDto>(user);
            userDto.Role = UserRoleGuidToRole(Guid.Parse(user!.UserRole!.RoleId));
            return userDto;
        }

        public async Task<IList<ApplicationUserDto>> LoadAll(CancellationToken cancellationToken)
        {
            var userEntities = await _userService.LoadAll(cancellationToken);
            var userDtos = _mapper.Map<IList<ApplicationUserDto>>(userEntities);
            foreach (var userEntity in userEntities)
            {
                var userDto = userDtos.Single(x => x.Id.ToString() == userEntity.Id);
                userDto.Role = UserRoleGuidToRole(Guid.Parse(userEntity.UserRole!.RoleId));
                userDto.Image ??= new();
            }
            return userDtos;
        }

        public async Task<ApplicationUserDto> Create(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<ApplicationUser>(user);
            var currentUser = await LoadCurrentUser(cancellationToken);
            CheckPermissionToCreate(currentUser, userEntity);

            var duplicities = await _userService.LoadDuplicities(userEntity, cancellationToken);
            if (duplicities.Count > 0)
            {
                throw new BadRequestException()
                {
                    InternalExceptionCode = ApiInternalExceptionCode.BAD_REQUEST,
                    Errors = new Dictionary<string, string[]>()
                    {{nameof(user),new string[] { (await _resourceService.Load(LanguageCode.EN,$"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.BadRequest}.{nameof(ApplicationUser)}.{UIExceptionConstants.UserExists}", cancellationToken)).Value}}}
                };
            }

            var insertedEntity = await _userService.Create(userEntity, user.Role, user.Password!, cancellationToken);
            var insertedDto = _mapper.Map<ApplicationUserDto>(insertedEntity);
            insertedDto.Role = UserRoleGuidToRole(Guid.Parse(insertedEntity.UserRole!.RoleId));

            return insertedDto;
        }

        public async Task<ApplicationUserDto> Update(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<ApplicationUser>(user);
            var currentUser = await LoadCurrentUser(cancellationToken);
            var editedUserEntity = await _userService.Load(user.Id!.Value, cancellationToken);

            CheckPermissionToUpdate(currentUser, editedUserEntity, userEntity);

            var newDuplicities = await _userService.LoadDuplicities(userEntity, cancellationToken);
            if (newDuplicities.Count > 0)
            {
                var isDuplicityOnlyWithSelf = newDuplicities.Count == 1 && newDuplicities.First().Id == userEntity.Id;
                if (!isDuplicityOnlyWithSelf)
                {
                    throw new BadRequestException()
                    {
                        InternalExceptionCode = ApiInternalExceptionCode.BAD_REQUEST,
                        Errors = new Dictionary<string, string[]>()
                        {{nameof(user),new string[] { (await _resourceService.Load(LanguageCode.EN,$"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.BadRequest}.{nameof(ApplicationUser)}.{UIExceptionConstants.UserExists}", cancellationToken)).Value}}}
                    };
                }
            }

            if (!string.IsNullOrWhiteSpace(user.Image?.ImageData) && !string.IsNullOrWhiteSpace(editedUserEntity!.Image?.ImageUrl))
            { _imageService.DeleteImage(editedUserEntity.Image); }

            _mapper.Map(userEntity, editedUserEntity);
            editedUserEntity = await _userService.Update(editedUserEntity!, user.Role, cancellationToken);

            var editedUserDto = _mapper.Map<ApplicationUserDto>(editedUserEntity);
            editedUserDto.Role = UserRoleGuidToRole(Guid.Parse(editedUserEntity.UserRole!.RoleId));
            editedUserDto.Image ??= new();

            return editedUserDto;
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var currentUser = await LoadCurrentUser(cancellationToken);
            var deletedEntity = await _userService.Load(id, cancellationToken);
            CheckPermissionsToDelete(currentUser, deletedEntity);

            await _userService.Delete(deletedEntity!, cancellationToken);
        }

        private UserRole UserRoleGuidToRole(Guid roleID)
        {
            if (_appSettings!.AuthSettings!.Defaults!.Roles!.UserGuid!.Value == roleID) { return UserRole.USER; }
            else if (_appSettings!.AuthSettings!.Defaults!.Roles!.AdminGuid!.Value == roleID) { return UserRole.ADMIN; }
            else if (_appSettings!.AuthSettings!.Defaults!.Roles!.SuperadminGuid!.Value == roleID) { return UserRole.OWNER; }

            return UserRole.USER;
        }
    }
}
