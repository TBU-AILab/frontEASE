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
            var buffer = new byte[SizeConstants.FiveMegabytes];
            using var bufferedStream = new BufferedStream(file.OpenReadStream(long.MaxValue), SizeConstants.FiveMegabytes);
            await bufferedStream.ReadAsync(buffer.AsMemory(0, SizeConstants.FiveMegabytes));
            return buffer.Take((int)file.Size).ToArray();
        }
    }
}
