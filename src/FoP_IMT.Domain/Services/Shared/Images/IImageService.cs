using FoP_IMT.Domain.Entities.Shared.Images;

namespace FoP_IMT.Domain.Services.Shared.Images
{
    public interface IImageService
    {
        Task SaveImage(Image image, Guid identifier);
        void DeleteImage(Image image);
    }
}
