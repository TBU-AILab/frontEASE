using FrontEASE.Shared.Data.DTOs.Management.Core.Packages;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Management
{
    /// <summary>
    /// DTO for transporting global preferences and nested submodels.
    /// </summary>
    public class GlobalPreferencesDto
    {
        public GlobalPreferencesDto()
        {
            CorePackages = [];
        }

        /// <summary>
        /// Core Packages
        /// </summary>
        [Resource($"{nameof(GlobalPreferencesDto)}.{nameof(CorePackages)}")]
        [RequiredValidation<GlobalPreferencesDto>]
        [CollectionNotEmptyValidation]
        public IList<GlobalPreferenceCorePackageDto> CorePackages { get; set; }
    }
}
