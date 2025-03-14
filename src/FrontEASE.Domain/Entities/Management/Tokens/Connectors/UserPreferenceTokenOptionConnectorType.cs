using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Management.Tokens;

namespace FrontEASE.Domain.Entities.Management.Tokens.Connectors
{
    public class UserPreferenceTokenOptionConnectorType : EntityBase
    {
        public UserPreferenceTokenOptionConnectorType()
        {
            ConnectorType = string.Empty;
            TokenOption = null!;
        }

        #region Navigation

        public Guid TokenOptionID { get; set; }
        public UserPreferenceTokenOption TokenOption { get; set; }

        #endregion

        #region Data

        public string ConnectorType { get; set; }

        #endregion
    }
}