using FoP_IMT.Domain.Entities.Base.Tracked;

namespace FoP_IMT.Domain.Entities.Management.Tokens
{
    public class UserPreferenceTokenOption : EntityTrackedBase
    {
        public UserPreferenceTokenOption()
        {
            this.Token = string.Empty;
            this.ConnectorType = string.Empty;
            this.Name = string.Empty;

            UserPreferences = null!;
        }

        #region Navigation

        public Guid UserPreferencesID { get; set; }
        public UserPreferences UserPreferences { get; set; }

        #endregion

        #region Data

        public int Priority { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string ConnectorType { get; set; }
        public string? Description { get; set; }

        #endregion
    }
}
