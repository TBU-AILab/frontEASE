using FrontEASE.Shared.Data.DTOs.Shared.Users;

namespace FrontEASE.Client.Services.ApiServices.Shared.Users
{
    public interface IUserApiService
    {
        Task<ApplicationUserDto> LoadUser();
        Task<IList<ApplicationUserDto>> LoadUsers();
        Task<ApplicationUserDto?> AddUser(ApplicationUserDto addUserDto);
        Task<ApplicationUserDto?> UpdateUser(ApplicationUserDto editUserDto);
        Task<bool> DeleteUser(Guid userID);
    }
}
