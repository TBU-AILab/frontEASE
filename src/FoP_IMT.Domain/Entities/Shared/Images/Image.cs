using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Shared.Data.Enums.Shared.Images;

namespace FoP_IMT.Domain.Entities.Shared.Images
{
    public class Image : EntityBase
    {
        public Image()
        {
            ImageData = string.Empty;
            ImageUrl = string.Empty;
            MimeType = string.Empty;
            Name = string.Empty;
        }

        #region Data

        public string ImageData { get; set; }
        public string ImageUrl { get; set; }
        public string MimeType { get; set; }
        public string Name { get; set; }
        public ImageType? Type { get; set; }

        #endregion
    }
}
