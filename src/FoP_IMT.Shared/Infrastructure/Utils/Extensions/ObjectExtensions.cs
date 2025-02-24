using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoP_IMT.Shared.Infrastructure.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static string Serialize(this object o)
        {
            var jsonSettings = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            return JsonSerializer.Serialize(o, jsonSettings);
        }
    }
}
