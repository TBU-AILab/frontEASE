using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoP_IMT.DataContracts.Converters.Tasks.Parameters
{
    public class ParameterEnumOptionConverter : JsonConverter<TaskModuleParameterEnumOptionCoreDto>
    {
        public override TaskModuleParameterEnumOptionCoreDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var enumOption = new TaskModuleParameterEnumOptionCoreDto();

            if (reader.TokenType == JsonTokenType.String)
            {
                enumOption.StringValue = reader.GetString();
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                var moduleValue = JsonSerializer.Deserialize<TaskModuleCoreDto>(ref reader, options);
                if (moduleValue is not null)
                {
                    enumOption.ModuleValue = moduleValue;
                }
            }
            else
            {
                throw new JsonException($"Unexpected token {reader.TokenType} when parsing {nameof(TaskModuleParameterEnumOptionCoreDto)}.");
            }

            return enumOption;
        }

        public override void Write(Utf8JsonWriter writer, TaskModuleParameterEnumOptionCoreDto value, JsonSerializerOptions options)
        {
            if (!string.IsNullOrEmpty(value.StringValue))
            {
                writer.WriteStringValue(value.StringValue);
            }
            else if (value.ModuleValue is not null)
            {
                JsonSerializer.Serialize(writer, value.ModuleValue, options);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
