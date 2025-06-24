using FrontEASE.Shared.Data.DTOs.Shared.Users;

namespace FrontEASE.Application.AppServices.Users
{
    public interface IUserAppService
    {
        Task<ApplicationUserDto> Load(CancellationToken cancellationToken);
        Task<IList<ApplicationUserDto>> LoadAll(CancellationToken cancellationToken);
        Task<ApplicationUserDto> Create(ApplicationUserDto user, CancellationToken cancellationToken);
        Task<ApplicationUserDto> Update(ApplicationUserDto user, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
