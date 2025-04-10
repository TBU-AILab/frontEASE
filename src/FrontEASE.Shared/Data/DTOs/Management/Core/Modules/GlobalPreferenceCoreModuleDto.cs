using FrontEASE.Shared.Data.DTOs.Shared.Files;
using FrontEASE.Shared.Infrastructure.Attributes;
using FrontEASE.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FrontEASE.Shared.Data.DTOs.Management.Core.Modules
{
    /// <summary>
    /// Class holding data about state of modules integration from the core application
    /// </summary>
    public class GlobalPreferenceCoreModuleDto
    {
        public GlobalPreferenceCoreModuleDto()
        {
            this.Files = [];
        }

        #region Data

        /// <summary>
        /// Nested file containing module
        /// </summary>
        [Resource($"{nameof(GlobalPreferenceCoreModuleDto)}.{nameof(Files)}")]
        [RequiredValidation<GlobalPreferenceCoreModuleDto>]
        [CollectionNotEmptyValidation]
        public IList<FileDto> Files { get; set; }

        #endregion
    }
}
