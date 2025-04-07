namespace FrontEASE.Domain.Entities.Management.Core.Packages
{
    public class GlobalPreferenceCorePackage
    {
        public GlobalPreferenceCorePackage()
        {
            this.Name = this.Version = string.Empty;
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public bool System { get; set; }
    }
}
