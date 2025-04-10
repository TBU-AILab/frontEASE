using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Shared.Files
{
    /// <summary>
    /// DTO for transferring files for upload purposes
    /// </summary>
    public class FileDto
    {
        public FileDto()
        {
            this.Name = this.MimeType = string.Empty;
            this.Content = [];
        }

        #region Data

        /// <summary>
        /// File name
        /// </summary>
        [Resource($"{nameof(FileDto)}.{nameof(Name)}")]
        [RequiredValidation<FileDto>]
        [StringLengthValidation(2, 256)]
        public string Name { get; set; }

        /// <summary>
        /// File mime type
        /// </summary>
        [Resource($"{nameof(FileDto)}.{nameof(MimeType)}")]
        [RequiredValidation<FileDto>]
        [StringLengthValidation(2, 256)]
        public string MimeType { get; set; }

        /// <summary>
        /// Image Contents (bytes)
        /// </summary>
        [Resource($"{nameof(FileDto)}.{nameof(Content)}")]
        [RequiredValidation<FileDto>]
        [CollectionNotEmptyValidation]
        public byte[] Content { get; set; }

        #endregion
    }
}
