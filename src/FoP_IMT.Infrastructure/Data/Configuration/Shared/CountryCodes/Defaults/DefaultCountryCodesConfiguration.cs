using FoP_IMT.Domain.Entities.Shared.CountryCodes;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;

namespace FoP_IMT.Infrastructure.Data.Configuration.Shared.CountryCodes.Defaults
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
