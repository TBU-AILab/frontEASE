using FoP_IMT.Shared.Data.Enums.Shared.Addresses;
using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FoP_IMT.Shared.Data.DTOs.Shared.Addresses
{
    /// <summary>
    /// DTO for transfer of address information
    /// </summary>
    public class AddressDto
    {
        public AddressDto()
        {
            Street = string.Empty;
            ZIPCode = string.Empty;
            City = string.Empty;
            DescriptiveNumber = string.Empty;
        }

        #region Data

        /// <summary>
        /// Address orientation number
        /// </summary>
        [Resource($"{nameof(AddressDto)}.{nameof(OrientationNumber)}")]
        [StringLengthValidation(0, 16)]
        public string? OrientationNumber { get; set; }

        /// <summary>
        /// Address description number
        /// </summary>
        [Resource($"{nameof(AddressDto)}.{nameof(DescriptiveNumber)}")]
        [RequiredValidation<AddressDto>]
        [StringLengthValidation(1, 12)]
        public string DescriptiveNumber { get; set; }

        /// <summary>
        /// Street name
        /// </summary>
        [Resource($"{nameof(AddressDto)}.{nameof(Street)}")]
        [RequiredValidation<AddressDto>]
        [StringLengthValidation(2, 64)]
        public string Street { get; set; }

        /// <summary>
        /// Location ZIP code
        /// </summary>
        [Resource($"{nameof(AddressDto)}.{nameof(ZIPCode)}")]
        [RequiredValidation<AddressDto>]
        [StringLengthValidation(4, 10)]
        public string ZIPCode { get; set; }

        /// <summary>
        /// Location city / district
        /// </summary>
        [Resource($"{nameof(AddressDto)}.{nameof(City)}")]
        [RequiredValidation<AddressDto>]
        [StringLengthValidation(2, 64)]
        public string City { get; set; }

        /// <summary>
        /// Location country
        /// </summary>
        [Resource($"{nameof(AddressDto)}.{nameof(Country)}")]
        [RequiredValidation<AddressDto>]
        [EnumValidation(typeof(Country))]
        public Country Country { get; set; }

        #endregion
    }
}
