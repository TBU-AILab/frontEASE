namespace FrontEASE.Domain.Entities.Management.Core.Modules
{
    public class GlobalPreferenceCoreModule
    {
        public GlobalPreferenceCoreModule()
        {
            this.Files = [];
        }

        #region Data

        public IList<Shared.Files.File> Files { get; set; }

        #endregion
    }
}
