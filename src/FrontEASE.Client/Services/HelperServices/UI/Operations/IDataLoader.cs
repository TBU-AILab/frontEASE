﻿using Blazorise;
using FrontEASE.Shared.Data.DTOs.Shared.Files;
using FrontEASE.Shared.Data.DTOs.Shared.Images;

namespace FrontEASE.Client.Services.HelperServices.UI.Operations
{
    public interface IDataLoader
    {
        Task LoadImage(FileChangedEventArgs args, ImageDto destination);
        Task LoadFiles(FileChangedEventArgs args, IList<FileDto> destination);
    }
}
