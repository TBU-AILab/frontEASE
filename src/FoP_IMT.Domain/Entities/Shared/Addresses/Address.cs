using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Shared.Data.Enums.Shared.Addresses;

namespace FoP_IMT.Domain.Entities.Shared.Addresses
{
    public class Address : EntityTrackedFull
    {
        public Address()
        {
            Street = string.Empty;
            ZIPCode = string.Empty;
            City = string.Empty;
        }

        #region Data

        public string? OrientationNumber {  get; set; }
        public string? DescriptiveNumber { get; set; }
        public string Street { get; set; }
        public string ZIPCode { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }

        #endregion
    }
}
