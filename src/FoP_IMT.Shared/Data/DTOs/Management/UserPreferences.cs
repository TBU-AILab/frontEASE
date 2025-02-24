using FoP_IMT.Shared.Data.DTOs.Management.General;
using FoP_IMT.Shared.Data.DTOs.Management.Tokens;
using FoP_IMT.Shared.Infrastructure.Attributes;

namespace FoP_IMT.Shared.Data.DTOs.Management
{
    /// <summary>
    /// DTO used for transporting user preferences
    /// </summary>
    public class UserPreferencesDto
    {
        public UserPreferencesDto()
        {
            GeneralOptions = new();
            TokenOptions = [];
        }

        [Resource($"{nameof(UserPreferencesDto)}.{nameof(GeneralOptions)}")]
        public UserPreferenceGeneralOptionsDto GeneralOptions { get; set; }

        /// <summary>
        /// Saved connector tokens
        /// </summary>
        [Resource($"{nameof(UserPreferencesDto)}.{nameof(TokenOptions)}")]
        public IList<UserPreferenceTokenOptionDto> TokenOptions { get; set; }
    }
}
