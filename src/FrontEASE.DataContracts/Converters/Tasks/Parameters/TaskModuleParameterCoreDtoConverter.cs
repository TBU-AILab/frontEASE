using FrontEASE.DataContracts.Constants;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using FrontEASE.Shared.Data.Enums.Tasks.Config.Modules.Parameters;
using FrontEASE.Shared.Infrastructure.Utils.Helpers.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Converters.Tasks.Parameters
{
    public class TaskModuleParameterCoreDtoConverter : JsonConverter<TaskModuleParameterCoreDto>
    {
        public override TaskModuleParameterCoreDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            using var document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;

            if (root.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException($"Expected JSON object for {nameof(TaskModuleParameterCoreDto)}.");
            }

            var typeString = root.TryGetProperty(ParameterDtoConstants.Type, out var typeElement) ? typeElement.GetString() ?? string.Empty : string.Empty;
            var paramType = DynamicParamUtils.GetParameterType(typeString);

            var dto = new TaskModuleParameterCoreDto
            {
                ShortName = root.TryGetProperty(ParameterDtoConstants.ShortName, out var shortNameElement) ? shortNameElement.GetString() ?? string.Empty : string.Empty,
                LongName = root.TryGetProperty(ParameterDtoConstants.LongName, out var longNameElement) ? longNameElement.GetString() : null,
                Description = root.TryGetProperty(ParameterDtoConstants.Description, out var descriptionElement) ? descriptionElement.GetString() : null,
                Type = typeString,
                MinValue = root.TryGetProperty(ParameterDtoConstants.MinValue, out var minValueElement) && minValueElement.ValueKind == JsonValueKind.Number && minValueElement.TryGetSingle(out var minValue) ? minValue : null,
                MaxValue = root.TryGetProperty(ParameterDtoConstants.MaxValue, out var maxValueElement) && maxValueElement.ValueKind == JsonValueKind.Number && maxValueElement.TryGetSingle(out var maxValue) ? maxValue : null,
                EnumDescriptions = root.TryGetProperty(ParameterDtoConstants.EnumDescriptions, out var enumDescriptionsElement) ? JsonSerializer.Deserialize<IList<string>>(enumDescriptionsElement.GetRawText(), options) : null,
                EnumLongNames = root.TryGetProperty(ParameterDtoConstants.EnumLongNames, out var enumLongNamesElement) ? JsonSerializer.Deserialize<IList<string>>(enumLongNamesElement.GetRawText(), options) : null,
                EnumOptions = root.TryGetProperty(ParameterDtoConstants.EnumOptions, out var enumOptionsElement) ? JsonSerializer.Deserialize<IList<TaskModuleParameterEnumOptionCoreDto>>(enumOptionsElement.GetRawText(), options) : null,
                Default = root.TryGetProperty(ParameterDtoConstants.Default, out var defaultElement) ? ReadParameterValue(defaultElement, paramType, options) : null,
                Value = root.TryGetProperty(ParameterDtoConstants.Value, out var valueElement) ? ReadParameterValue(valueElement, paramType, options) : null,
                Readonly = root.TryGetProperty(ParameterDtoConstants.Readonly, out var readonlyElement) && (readonlyElement.ValueKind == JsonValueKind.True || readonlyElement.ValueKind == JsonValueKind.False) ? readonlyElement.GetBoolean() : null,
                Required = root.TryGetProperty(ParameterDtoConstants.Required, out var requiredElement) && (requiredElement.ValueKind == JsonValueKind.True || requiredElement.ValueKind == JsonValueKind.False) ? requiredElement.GetBoolean() : null
            };

            return dto;
        }

        private static TaskModuleParameterValueCoreDto? ReadParameterValue(JsonElement element, ParameterType? paramType, JsonSerializerOptions options)
        {
            if (element.ValueKind == JsonValueKind.Null || paramType is null)
            {
                return null;
            }

            var valueDto = new TaskModuleParameterValueCoreDto();
            switch (paramType)
            {
                case ParameterType.INT:
                case ParameterType.TIME:
                    if (element.TryGetInt32(out var intValue))
                    {
                        valueDto.IntValue = intValue;
                    }
                    break;
                case ParameterType.FLOAT:
                    if (element.TryGetSingle(out var floatValue))
                    {
                        valueDto.FloatValue = floatValue;
                    }
                    else if (element.TryGetDouble(out var doubleValue))
                    {
                        valueDto.FloatValue = (float)doubleValue;
                    }
                    break;
                case ParameterType.STR:
                case ParameterType.MARKDOWN:
                case ParameterType.BYTES:
                    valueDto.StringValue = element.GetString();
                    break;
                case ParameterType.BOOL:
                    valueDto.BoolValue = element.GetBoolean();
                    break;
                case ParameterType.LIST:
                    var listOption = new TaskModuleParameterListOptionCoreDto();
                    foreach (var item in element.EnumerateArray())
                    {
                        var listItem = new Dictionary<string, TaskModuleParameterCoreDto>();
                        foreach (var property in item.EnumerateObject())
                        {
                            var parameter = JsonSerializer.Deserialize<TaskModuleParameterCoreDto>(property.Value.GetRawText(), options);
                            if (parameter is not null)
                            {
                                listItem[property.Name] = parameter;
                            }
                        }
                        if (listItem.Count > 0)
                        {
                            listOption.ParameterValues.Add(listItem);
                        }
                    }
                    valueDto.ListValue = listOption;
                    break;
                case ParameterType.ENUM:
                    valueDto.EnumValue = JsonSerializer.Deserialize<TaskModuleParameterEnumOptionCoreDto>(element.GetRawText(), options);
                    break;
            }

            return valueDto;
        }

        public override void Write(Utf8JsonWriter writer, TaskModuleParameterCoreDto dto, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString(ParameterDtoConstants.ShortName, dto.ShortName);

            if (!string.IsNullOrEmpty(dto.LongName)) { writer.WriteString(ParameterDtoConstants.LongName, dto.LongName); }
            if (!string.IsNullOrEmpty(dto.Description)) { writer.WriteString(ParameterDtoConstants.Description, dto.Description); }
            writer.WriteString(ParameterDtoConstants.Type, dto.Type);
            if (dto.MinValue.HasValue) { writer.WriteNumber(ParameterDtoConstants.MinValue, dto.MinValue.Value); }
            if (dto.MaxValue.HasValue) { writer.WriteNumber(ParameterDtoConstants.MaxValue, dto.MaxValue.Value); }

            if (dto.EnumDescriptions is not null)
            {
                writer.WritePropertyName(ParameterDtoConstants.EnumDescriptions);
                JsonSerializer.Serialize(writer, dto.EnumDescriptions, options);
            }

            if (dto.EnumLongNames is not null)
            {
                writer.WritePropertyName(ParameterDtoConstants.EnumLongNames);
                JsonSerializer.Serialize(writer, dto.EnumLongNames, options);
            }

            if (dto.EnumOptions is not null)
            {
                writer.WritePropertyName(ParameterDtoConstants.EnumOptions);
                JsonSerializer.Serialize(writer, dto.EnumOptions, options);
            }

            WriteParameterValue(writer, ParameterDtoConstants.Default, dto.Default, options);
            WriteParameterValue(writer, ParameterDtoConstants.Value, dto.Value, options);
            if (dto.Readonly.HasValue) { writer.WriteBoolean(ParameterDtoConstants.Readonly, dto.Readonly.Value); }
            if (dto.Required.HasValue) { writer.WriteBoolean(ParameterDtoConstants.Required, dto.Required.Value); }

            writer.WriteEndObject();
        }

        private static void WriteParameterValue(Utf8JsonWriter writer, string propertyName, TaskModuleParameterValueCoreDto? value, JsonSerializerOptions options)
        {
            writer.WritePropertyName(propertyName);
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            if (value.FloatValue.HasValue) { writer.WriteNumberValue(value.FloatValue.Value); }
            else if (value.IntValue.HasValue) { writer.WriteNumberValue(value.IntValue.Value); }
            else if (!string.IsNullOrEmpty(value.StringValue)) { writer.WriteStringValue(value.StringValue); }
            else if (value.BoolValue.HasValue) { writer.WriteBooleanValue(value.BoolValue.Value); }
            else if (value.ListValue is not null) { JsonSerializer.Serialize(writer, value.ListValue.ParameterValues, options); }
            else if (value.EnumValue is not null) { JsonSerializer.Serialize(writer, value.EnumValue, options); }
            else { writer.WriteNullValue(); }
        }
    }
}
