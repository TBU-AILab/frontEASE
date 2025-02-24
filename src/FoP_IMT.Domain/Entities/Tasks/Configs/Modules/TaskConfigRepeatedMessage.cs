using FoP_IMT.Domain.Entities.Base.Tracked;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.RepeatedMessage;
using FoP_IMT.Shared.Data.Enums.Tasks.Config.Modules.RepeatedMessage;

namespace FoP_IMT.Domain.Entities.Tasks.Configs.Modules
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
