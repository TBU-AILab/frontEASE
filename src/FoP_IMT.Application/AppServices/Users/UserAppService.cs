using AutoMapper;
using FoP_IMT.Domain.Entities.Shared.Users;
using FoP_IMT.Domain.Infrastructure.Exceptions.Enums;
using FoP_IMT.Domain.Infrastructure.Exceptions.Types;
using FoP_IMT.Domain.Infrastructure.Settings.App;
using FoP_IMT.Domain.Services.Shared.Images;
using FoP_IMT.Domain.Services.Shared.Resources;
using FoP_IMT.Domain.Services.Users;
using FoP_IMT.Shared.Data.DTOs.Shared.Users;
using FoP_IMT.Shared.Data.Enums.Auth;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;
using FoP_IMT.Shared.Infrastructure.Constants.UI.Generic;
using FoP_IMT.Shared.Infrastructure.Constants.UI.Specific;
using System.Net;

namespace FoP_IMT.Application.AppServices.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly IResourceService _resourceService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserAppService(
            IUserService userService,
            IImageService imageService,
            IResourceService resourceService,
            IMapper mapper,
            AppSettings appSettings)
        {
            _imageService = imageService;
            _resourceService = resourceService;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public async Task<IList<ApplicationUserDto>> LoadAll()
        {
            var userEntities = await _userService.LoadAll();
            var userDtos = _mapper.Map<IList<ApplicationUserDto>>(userEntities);
            foreach (var userEntity in userEntities)
            {
                var userDto = userDtos.Single(x => x.Id.ToString() == userEntity.Id);
                userDto.Role = UserRoleGuidToRole(Guid.Parse(userEntity.UserRole!.RoleId));
                userDto.Image ??= new();
            }
            return userDtos;
        }

        public async Task<ApplicationUserDto> Create(ApplicationUserDto user)
        {
            var userEntity = _mapper.Map<ApplicationUser>(user);
            var duplicities = await _userService.LoadDuplicities(userEntity);
            if (duplicities.Count > 0)
            {
                throw new BadRequestException()
                {
                    InternalExceptionCode = ApiInternalExceptionCode.BAD_REQUEST,
                    Errors = new Dictionary<string, string[]>()
                    {{nameof(user),new string[] { (await _resourceService.Load(LanguageCode.EN,$"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.BadRequest}.{nameof(ApplicationUser)}.{UIExceptionConstants.UserExists}")).Value}}}
                };
            }

            var insertedEntity = await _userService.Create(userEntity, user.Role, user.Password!);
            var insertedDto = _mapper.Map<ApplicationUserDto>(insertedEntity);
            insertedDto.Role = UserRoleGuidToRole(Guid.Parse(insertedEntity.UserRole!.RoleId));

            return insertedDto;
        }

        public async Task<ApplicationUserDto> Update(ApplicationUserDto user)
        {
            var userEntity = _mapper.Map<ApplicationUser>(user);

            var newDuplicities = await _userService.LoadDuplicities(userEntity);
            if (newDuplicities.Count > 0)
            {
                var isDuplicityOnlyWithSelf = newDuplicities.Count == 1 && newDuplicities.First().Id == userEntity.Id;
                if (!isDuplicityOnlyWithSelf)
                {
                    throw new BadRequestException()
                    {
                        InternalExceptionCode = ApiInternalExceptionCode.BAD_REQUEST,
                        Errors = new Dictionary<string, string[]>()
                        {{nameof(user),new string[] { (await _resourceService.Load(LanguageCode.EN,$"{UIConstants.Data}.{UIConstants.Error}.{HttpStatusCode.BadRequest}.{nameof(ApplicationUser)}.{UIExceptionConstants.UserExists}")).Value}}}
                    };
                }
            }

            var editedUserEntity = await _userService.Load(user.Id!.Value);

            if (!string.IsNullOrWhiteSpace(user.Image?.ImageData) && !string.IsNullOrWhiteSpace(editedUserEntity!.Image?.ImageUrl))
            { _imageService.DeleteImage(editedUserEntity.Image); }

            _mapper.Map(userEntity, editedUserEntity);
            editedUserEntity = await _userService.Update(editedUserEntity!, user.Role);

            var editedUserDto = _mapper.Map<ApplicationUserDto>(editedUserEntity);
            editedUserDto.Role = UserRoleGuidToRole(Guid.Parse(editedUserEntity.UserRole!.RoleId));
            editedUserDto.Image ??= new();

            return editedUserDto;
        }

        public async Task Delete(Guid id)
        {
            var deletedEntity = await _userService.Load(id);
            await _userService.Delete(deletedEntity!);
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
