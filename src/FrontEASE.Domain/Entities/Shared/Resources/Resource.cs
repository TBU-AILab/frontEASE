using FrontEASE.Domain.Entities.Shared.CountryCodes;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Domain.Entities.Shared.Resources
{
    public class Resource
    {
        public Resource()
        {
            ResourceCode = Value = string.Empty;
            CountryCodeID = LanguageCode.EN;
        }

        #region Navigation

        public LanguageCode CountryCodeID { get; set; }
        public CountryCode? CountryCode { get; set; }

        #endregion

        #region Data

        public string ResourceCode { get; set; }
        public string Value { get; set; }

        #endregion
    }
}
