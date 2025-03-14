using FrontEASE.Shared.Data.DTOs.Management.General;
using FrontEASE.Shared.Data.DTOs.Management.Tokens;
using FrontEASE.Shared.Infrastructure.Attributes;

namespace FrontEASE.Shared.Data.DTOs.Management
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
