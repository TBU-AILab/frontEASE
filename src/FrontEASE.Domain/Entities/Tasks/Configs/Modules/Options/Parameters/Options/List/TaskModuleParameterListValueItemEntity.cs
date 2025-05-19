using FrontEASE.Domain.Entities.Base.Tracked;

namespace FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List
{
    public class TaskModuleParameterListValueItemEntity : EntityBase
    {
        public TaskModuleParameterListValueItemEntity()
        {
            ListParamValue = null!;
            ParameterItems = [];
        }

        #region Navigation

        public Guid? ListParamValueID { get; set; }
        public TaskModuleParameterListValueEntity ListParamValue { get; set; }


        public IList<TaskModuleParameterEntity> ParameterItems { get; set; }

        #endregion
    }
}
