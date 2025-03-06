using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Shared.Data.Enums.Shared.Images;

namespace FrontEASE.Domain.Entities.Shared.Images
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
