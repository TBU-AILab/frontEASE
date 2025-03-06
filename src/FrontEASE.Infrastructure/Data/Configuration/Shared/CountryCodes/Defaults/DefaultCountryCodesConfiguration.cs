using FrontEASE.Domain.Entities.Shared.CountryCodes;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Infrastructure.Data.Configuration.Shared.CountryCodes.Defaults
{
    public class DefaultCountryCodesConfiguration
    {
        public static IList<CountryCode> GetDefaults()
        {
            var countryCodes = new List<CountryCode>()
            {
                new CountryCode() { ID = LanguageCode.EN, Name = LanguageCode.EN.ToString() }
            };
            return countryCodes;
        }
    }
}
