using FrontEASE.Shared.Data.DTOs.Shared.Addresses;
using FrontEASE.Shared.Data.DTOs.Shared.Images;
using FrontEASE.Shared.Data.DTOs.Shared.Users;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Companies
{
    /// <summary>
    /// DTO for transfer of company information
    /// </summary>
    public class CompanyDto
    {
        public CompanyDto()
        {
            Image = new ImageDto();
            Name = string.Empty;
            NameAbbreviation = string.Empty;
            Users = [];
        }

        #region Navigation

        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// Nested object - Address
        /// </summary>
        [Resource($"{nameof(CompanyDto)}.{nameof(Address)}")]
        public AddressDto? Address { get; set; }

        /// <summary>
        /// Nested object - Image
        /// </summary>
        [Resource($"{nameof(CompanyDto)}.{nameof(Image)}")]
        public ImageDto? Image { get; set; }

        /// <summary>
        /// List of users affiliated with this company.
        /// </summary>
        [Resource($"{nameof(CompanyDto)}.{nameof(Users)}")]
        [RequiredValidation<CompanyDto>]
        public IList<ApplicationUserDto> Users { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Company name
        /// </summary>
        [Resource($"{nameof(CompanyDto)}.{nameof(Name)}")]
        [RequiredValidation<CompanyDto>]
        [StringLengthValidation(2, 128)]
        public string Name { get; set; }

        /// <summary>
        /// Company name abbreviation
        /// </summary>
        [Resource($"{nameof(CompanyDto)}.{nameof(NameAbbreviation)}")]
        [RequiredValidation<CompanyDto>]
        [StringLengthValidation(2, 16)]
        public string NameAbbreviation { get; set; }

        #endregion
    }
}
