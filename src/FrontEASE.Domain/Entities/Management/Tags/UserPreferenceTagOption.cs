using FrontEASE.Domain.Entities.Base.Tracked;

namespace FrontEASE.Domain.Entities.Management.Tags
{
    public class UserPreferenceTagOption : EntityBase
    {
        public UserPreferenceTagOption()
        {
            Tag = string.Empty;

            UserPreferences = null!;
            Tasks = [];
        }

        #region Navigation

        public Guid UserPreferencesID { get; set; }
        public UserPreferences UserPreferences { get; set; }

        public IList<Tasks.Task> Tasks { get; set; }

        #endregion

        #region Data

        public string Tag { get; set; }

        #endregion
    }
}
