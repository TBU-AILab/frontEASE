using FoP_IMT.Domain.Entities.Shared.Images;
using FoP_IMT.Shared.Data.Enums.Shared.Images;
using Microsoft.AspNetCore.Hosting;

namespace FoP_IMT.Domain.Services.Shared.Images
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _webHostEnvironment;
        public ImageService(IHostingEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task SaveImage(Image image, Guid identifier)
        {
            if (image is not null)
            {
                var path = GetImagePath(image, identifier);
                var directory = Path.GetDirectoryName(path.Absolute);

                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory!); }
                if (File.Exists(path.Absolute)) { File.Delete(path.Absolute); }

                var imageBytes = Convert.FromBase64String(image.ImageData);
                await File.WriteAllBytesAsync(path.Absolute, imageBytes);

                image.ImageUrl = path.Relative;
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
