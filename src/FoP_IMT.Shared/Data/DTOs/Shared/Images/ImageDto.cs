using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FoP_IMT.Shared.Data.DTOs.Shared.Images
{
    /// <summary>
    /// DTO for transport of base64 profile / view image
    /// </summary>
    public class ImageDto
    {
        public ImageDto()
        {
            ImageData = string.Empty;
            MimeType = string.Empty;
            Name = string.Empty;
        }

        #region Data

        /// <summary>
        /// Image url - saved image
        /// </summary>
        [Resource($"{nameof(ImageDto)}.{nameof(ImageUrl)}")]
        [StringLengthValidation(0, 256)]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Image data - base64
        /// </summary>
        [Resource($"{nameof(ImageDto)}.{nameof(ImageData)}")]
        [StringLengthValidation(0, 8000000)]
        [Base64StringValidation]
        public string? ImageData { get; set; }

        /// <summary>
        /// Standardized MIME type for image
        /// </summary>
        [Resource($"{nameof(ImageDto)}.{nameof(MimeType)}")]
        [RequiredValidation<ImageDto>]
        [StringLengthValidation(2, 32)]
        public string MimeType { get; set; }

        /// <summary>
        /// Image name
        /// </summary>
        [Resource($"{nameof(ImageDto)}.{nameof(Name)}")]
        [RequiredValidation<ImageDto>]
        [StringLengthValidation(2, 256)]
        public string Name { get; set; }

        #endregion
    }
}
