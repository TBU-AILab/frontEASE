using FrontEASE.Domain.Entities.Shared.Resources;
using FrontEASE.Shared.Data.Enums.Shared.Resources;

namespace FrontEASE.Domain.Entities.Shared.CountryCodes
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
