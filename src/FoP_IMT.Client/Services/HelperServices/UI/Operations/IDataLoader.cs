using Blazorise;
using FoP_IMT.Shared.Data.DTOs.Shared.Images;

namespace FoP_IMT.Client.Services.HelperServices.UI.Operations
{
    public interface IDataLoader
    {
        Task LoadImage(FileChangedEventArgs args, ImageDto destination);
    }
}
