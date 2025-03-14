using FrontEASE.Domain.Entities.Base.Tracked;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.RepeatedMessage;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.RepeatedMessage;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules
{
    public class TaskConfigRepeatedMessage : EntityBase
    {
        public TaskConfigRepeatedMessage()
        {
            TaskConfig = null!;

            RepeatedMessageItems = [];
        }

        #region Navigation

        public TaskConfig TaskConfig { get; set; }
        public IList<TaskConfigRepeatedMessageItem> RepeatedMessageItems { get; set; }

        #endregion

        #region Data

        public RepeatedMessageType RepeatedMessageType { get; set; }

        #endregion
    }
}
