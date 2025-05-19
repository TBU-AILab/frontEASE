using AutoMapper;
using Blazorise;
using FrontEASE.Shared.Data.DTOs.Shared.Files;
using FrontEASE.Shared.Data.DTOs.Shared.Images;
using FrontEASE.Shared.Infrastructure.Constants.Control;

namespace FrontEASE.Client.Services.HelperServices.UI.Operations
{
    public class DataLoader : IDataLoader
    {
        private readonly IMapper _mapper;

        public DataLoader(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task LoadImage(FileChangedEventArgs args, ImageDto destination)
        {
            var file = args.Files.FirstOrDefault();
            if (file is not null)
            {
                var fileData = await ReadFileData(file);
                var resultImage = new ImageDto
                {
                    ImageData = Convert.ToBase64String(fileData),
                    MimeType = file.Type,
                    Name = file.Name
                };
                _mapper.Map(resultImage, destination);
            }
        }

        public async Task LoadFiles(FileChangedEventArgs args, IList<FileDto> destination)
        {
            destination.Clear();

            foreach (var file in args.Files)
            {
                if (file is not null)
                {
                    var fileData = await ReadFileData(file);
                    var resultFile = new FileDto
                    {
                        Content = fileData,
                        MimeType = file.Type,
                        Name = file.Name
                    };
                    destination.Insert(0, resultFile);
                }
            }
        }

        private async Task<byte[]> ReadFileData(IFileEntry file)
        {
            var fileSize = (int)file.Size;
            var buffer = new byte[fileSize];
            using var bufferedStream = new BufferedStream(file.OpenReadStream(file.Size), SizeConstants.FiveMegabytes);
            var totalRead = 0;
            while (totalRead < fileSize)
            {
                int bytesRead = await bufferedStream.ReadAsync(buffer.AsMemory(totalRead, fileSize - totalRead));
                if (bytesRead == 0)
                {
                    break;
                }
                totalRead += bytesRead;
            }
            return buffer;
        }
    }
}
