using FrontEASE.Domain.Entities.Base.Tracked;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.RepeatedMessage
{
    public class TaskConfigRepeatedMessageItem : EntityBase
    {
        public TaskConfigRepeatedMessageItem()
        {
            RepeatedMessage = null!;

            Content = string.Empty;
        }

        #region Navigation

        public Guid RepeatedMessageID { get; set; }
        public TaskConfigRepeatedMessage RepeatedMessage { get; set; }

        #endregion

        #region Data

        public string Content { get; set; }
        public float Weight { get; set; }

        #endregion
    }
}
