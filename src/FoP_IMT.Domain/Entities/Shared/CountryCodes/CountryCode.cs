using FoP_IMT.Domain.Entities.Shared.Resources;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;

namespace FoP_IMT.Domain.Entities.Shared.CountryCodes
{
    public class CountryCode
    {
        public CountryCode()
        {
            Name = string.Empty;
            Resources = [];
        }

        #region Navigation

        public LanguageCode ID { get; set; }
        public IList<Resource> Resources { get; set; }

        #endregion

        #region Data

        public string Name { get; set; }

        #endregion
    }
}
