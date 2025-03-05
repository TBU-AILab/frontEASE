using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Converters.Tasks.Parameters
{
    public class TaskModuleParameterValueConverter : JsonConverter<TaskModuleParameterValueCoreDto>
    {
        public override TaskModuleParameterValueCoreDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var dto = new TaskModuleParameterValueCoreDto();

            using (JsonDocument document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;

                if (root.ValueKind == JsonValueKind.Number)
                {
                    if (root.TryGetInt32(out int intValue))
                    {
                        dto.IntValue = intValue;
                    }
                    else if (root.TryGetDouble(out double doubleValue))
                    {
                        dto.FloatValue = (float)doubleValue;
                    }
                }
                else if (root.ValueKind == JsonValueKind.String)
                {
                    dto.StringValue = root.GetString();
                }
                else if (root.ValueKind == JsonValueKind.True || root.ValueKind == JsonValueKind.False)
                {
                    dto.BoolValue = root.GetBoolean();
                }
                else if (root.ValueKind == JsonValueKind.Object)
                {
                    dto.EnumValue = JsonSerializer.Deserialize<TaskModuleParameterEnumOptionCoreDto>(root.GetRawText(), options);
                }
            }

            return dto;
        }

        public override void Write(Utf8JsonWriter writer, TaskModuleParameterValueCoreDto value, JsonSerializerOptions options)
        {
            if (value.FloatValue.HasValue)
            {
                writer.WriteNumberValue(value.FloatValue.Value);
            }
            else if (value.IntValue.HasValue)
            {
                writer.WriteNumberValue(value.IntValue.Value);
            }
            else if (!string.IsNullOrEmpty(value.StringValue))
            {
                writer.WriteStringValue(value.StringValue);
            }
            else if (value.BoolValue.HasValue)
            {
                writer.WriteBooleanValue(value.BoolValue.Value);
            }
            else if (value.EnumValue is not null)
            {
                JsonSerializer.Serialize(writer, value.EnumValue, options);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
