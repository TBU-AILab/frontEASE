using FoP_IMT.Shared.Data.DTOs.Shared.Users;

namespace FoP_IMT.Client.Services.ApiServices.Shared.Users
{
    public interface IUserApiService
    {
        Task<IList<ApplicationUserDto>> LoadUsers();
        Task<ApplicationUserDto?> AddUser(ApplicationUserDto addUserDto);
        Task<ApplicationUserDto?> UpdateUser(ApplicationUserDto editUserDto);
        Task<bool> DeleteUser(Guid userID);
    }
}
