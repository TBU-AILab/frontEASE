using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Management.Tokens.Connectors;

namespace FrontEASE.Domain.Entities.Management.Tokens
{
    public class UserPreferenceTokenOption : EntityTrackedBase
    {
        public UserPreferenceTokenOption()
        {
            Token = string.Empty;
            Name = string.Empty;

            UserPreferences = null!;
            ConnectorTypes = [];
        }

        #region Navigation

        public Guid UserPreferencesID { get; set; }
        public UserPreferences UserPreferences { get; set; }
        public IList<UserPreferenceTokenOptionConnectorType> ConnectorTypes { get; set; }

        #endregion

        #region Data

        public int Priority { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string? Description { get; set; }

        #endregion
    }
}