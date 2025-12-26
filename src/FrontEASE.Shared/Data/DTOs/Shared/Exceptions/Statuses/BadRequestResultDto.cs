using System.Net;

namespace FrontEASE.Shared.Data.DTOs.Shared.Exceptions.Statuses
{
    /// <summary>
    /// Model transporting information about incorrectly performed API call
    /// </summary>
    public class BadRequestResultDto : StatusResultDto
    {
        public BadRequestResultDto()
        {
            StatusCode = HttpStatusCode.BadRequest;

            Errors = [];
        }

        /// <summary>
        /// List of reasons for call invalidity
        /// </summary>
        public IList<BadRequestErrorReason> Errors { get; set; }
    }


    /// <summary>
    /// Validation message returned as a reasoning for 400 - HTTP code
    /// </summary>
    public partial class BadRequestErrorReason
    {
        public BadRequestErrorReason()
        {
            Key = string.Empty;
            Problems = [];
        }

        /// <summary>
        /// Invalidity source - field name
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// List of validation problems linked to this field
        /// </summary>
        public IList<string> Problems { get; set; }
    }
}
