using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrontEASE.Shared.Infrastructure.Utils.Extensions
{
    public static class ObjectExtensions
    {
        private static readonly JsonSerializerOptions JsonSettings = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public static string Serialize(this object o) => JsonSerializer.Serialize(o, JsonSettings);
    }
}
