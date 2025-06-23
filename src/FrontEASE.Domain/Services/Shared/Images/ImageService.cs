using FrontEASE.Domain.Entities.Shared.Images;
using FrontEASE.Shared.Data.Enums.Shared.Images;
using Microsoft.AspNetCore.Hosting;

namespace FrontEASE.Domain.Services.Shared.Images
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task SaveImage(Image image, Guid identifier, CancellationToken cancellationToken)
        {
            if (image is not null)
            {
                var (Relative, Absolute) = GetImagePath(image, identifier);
                var directory = Path.GetDirectoryName(Absolute);

                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory!); }
                if (File.Exists(Absolute)) { File.Delete(Absolute); }

                var imageBytes = Convert.FromBase64String(image.ImageData);
                await File.WriteAllBytesAsync(Absolute, imageBytes, cancellationToken);

                image.ImageUrl = Relative;
            }
        }

        public void DeleteImage(Image image)
        {
            if (image is not null)
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, image.ImageUrl);
                if (File.Exists(path)) { File.Delete(path); }

                image.ImageData = image.ImageUrl = string.Empty;
            }
        }


        private (string Relative, string Absolute) GetImagePath(Image image, Guid identifier)
        {
            var directory = $"resources{Path.DirectorySeparatorChar}images";
            switch (image.Type)
            {
                case ImageType.COMPANY_LOGO:
                    directory =
                        $"{directory}" +
                        $"{Path.DirectorySeparatorChar}" +
                        $"companies" +
                        $"{Path.DirectorySeparatorChar}" +
                        $"profiles";
                    break;
                case ImageType.USER_PROFILE_PICTURE:
                    directory =
                        $"{directory}" +
                        $"{Path.DirectorySeparatorChar}" +
                        $"users" +
                        $"{Path.DirectorySeparatorChar}" +
                        $"profiles";
                    break;
            }

            var fileName = $"{identifier}_{image.Name}";
            var relativePath = $"{directory}{Path.DirectorySeparatorChar}{fileName}";
            var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, directory, fileName);

            return (relativePath, absolutePath);
        }
    }
}
