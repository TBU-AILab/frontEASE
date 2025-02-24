using FoP_IMT.Shared.Data.DTOs.Shared.Users;

namespace FoP_IMT.Application.AppServices.Users
{
    public interface IUserAppService
    {
        Task<IList<ApplicationUserDto>> LoadAll();
        Task<ApplicationUserDto> Create(ApplicationUserDto user);
        Task<ApplicationUserDto> Update(ApplicationUserDto user);
        Task Delete(Guid id);
    }
}
