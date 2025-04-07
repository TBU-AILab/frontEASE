using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Management.Core.Packages
{
    /// <summary>
    /// Class holding data about state of packages installation from the application
    /// </summary>
    public class GlobalPreferenceCorePackageDto
    {
        public GlobalPreferenceCorePackageDto()
        {
            this.Name = this.Version = string.Empty;
        }

        /// <summary>
        /// Package name (Python-syntax).
        /// </summary>
        [Resource($"{nameof(GlobalPreferenceCorePackageDto)}.{nameof(Name)}")]
        [RequiredValidation<GlobalPreferenceCorePackageDto>]
        [StringLengthValidation(2, 128)]
        public string Name { get; set; }

        /// <summary>
        /// Package version.
        /// </summary>
        [Resource($"{nameof(GlobalPreferenceCorePackageDto)}.{nameof(Version)}")]
        [RequiredValidation<GlobalPreferenceCorePackageDto>]
        [StringLengthValidation(2, 64)]
        public string Version { get; set; }

        /// <summary>
        /// Is system (core) package - cannot be uninstalled
        /// </summary>
        [RequiredValidation<GlobalPreferenceCorePackageDto>]
        public bool System { get; set; }
    }
}
