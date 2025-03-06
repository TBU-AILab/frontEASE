using AutoMapper;
using Blazorise;
using FrontEASE.Shared.Data.DTOs.Shared.Images;

namespace FrontEASE.Client.Services.HelperServices.UI.Operations
{
    public class DataLoader : IDataLoader
    {
        public static int MaxImageSize = 5 * 1024 * 1024;

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
                var buffer = new byte[MaxImageSize];
                using var bufferedStream = new BufferedStream(file.OpenReadStream(long.MaxValue), MaxImageSize);
                await bufferedStream.ReadAsync(buffer.AsMemory(0, MaxImageSize));
                var usedDataPart = buffer.Take((int)file.Size).ToArray();

                var resultImage = new ImageDto()
                {
                    ImageData = Convert.ToBase64String(usedDataPart),
                    MimeType = file.Type,
                    Name = file.Name
                };
                _mapper.Map(resultImage, destination);
            }
        }
    }
}
