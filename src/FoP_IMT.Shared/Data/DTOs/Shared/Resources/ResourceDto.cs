using FoP_IMT.Shared.Infrastructure.Attributes;
using FoP_IMT.Shared.Infrastructure.Attributes.Validations.Generic;

namespace FoP_IMT.Shared.Data.DTOs.Shared.Resources
{
    /// <summary>
    /// Dto for transporting text resources
    /// </summary>
    public class ResourceDto
    {
        public ResourceDto()
        {
            ResourceCode = Value = string.Empty;
        }

        /// <summary>
        /// Code name (identifier) of resource
        /// </summary>
        [Resource($"{nameof(ResourceDto)}.{nameof(ResourceCode)}")]
        [StringLengthValidation(2, 256)]
        [RequiredValidation<ResourceDto>]
        public string ResourceCode { get; set; }

        /// <summary>
        /// Text value of resource
        /// </summary>
        [Resource($"{nameof(ResourceDto)}.{nameof(Value)}")]
        [StringLengthValidation(2, 2048)]
        [RequiredValidation<ResourceDto>]
        public string Value { get; set; }
    }
}
