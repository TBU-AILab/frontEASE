using FrontEASE.Domain.Entities.Management.Core.Packages;

namespace FrontEASE.Domain.Entities.Management
{
    public class GlobalPreferences
    {
        public GlobalPreferences()
        {
            CorePackages = [];
        }
        public IList<GlobalPreferenceCorePackage> CorePackages { get; set; }
    }
}