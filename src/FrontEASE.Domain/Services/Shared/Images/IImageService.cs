using FrontEASE.Domain.Entities.Shared.Images;

namespace FrontEASE.Domain.Services.Shared.Images
{
    public interface IImageService
    {
        Task SaveImage(Image image, Guid identifier);
        void DeleteImage(Image image);
    }
}
