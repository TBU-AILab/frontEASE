﻿using FrontEASE.Shared.Data.DTOs.Shared.Users;

namespace FrontEASE.Application.AppServices.Users
{
    public interface IUserAppService
    {
        Task<ApplicationUserDto> Load();
        Task<IList<ApplicationUserDto>> LoadAll();
        Task<ApplicationUserDto> Create(ApplicationUserDto user);
        Task<ApplicationUserDto> Update(ApplicationUserDto user);
        Task Delete(Guid id);
    }
}
