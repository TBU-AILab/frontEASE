using FrontEASE.DataContracts.Constants;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrontEASE.DataContracts.Converters.Tasks.Parameters
{
    public class TaskModuleParameterCoreDtoConverter : JsonConverter<TaskModuleParameterCoreDto>
    {
        public override TaskModuleParameterCoreDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            using var document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;

            if (root.ValueKind != JsonValueKind.Object)
                throw new JsonException($"Expected JSON object for {nameof(TaskModuleParameterCoreDto)}.");

            var dto = new TaskModuleParameterCoreDto();

            if (root.TryGetProperty(ParameterDtoConstants.ShortName, out JsonElement shortNameElement))
                dto.ShortName = shortNameElement.GetString() ?? string.Empty;

            if (root.TryGetProperty(ParameterDtoConstants.LongName, out JsonElement longNameElement))
                dto.LongName = longNameElement.GetString();

            if (root.TryGetProperty(ParameterDtoConstants.Description, out JsonElement descriptionElement))
                dto.Description = descriptionElement.GetString();

            if (root.TryGetProperty(ParameterDtoConstants.Type, out JsonElement typeElement))
                dto.Type = typeElement.GetString() ?? string.Empty;

            if (root.TryGetProperty(ParameterDtoConstants.MinValue, out JsonElement minValueElement) &&
                minValueElement.ValueKind == JsonValueKind.Number &&
                minValueElement.TryGetSingle(out float minValue))
                dto.MinValue = minValue;

            if (root.TryGetProperty(ParameterDtoConstants.MaxValue, out JsonElement maxValueElement) &&
                maxValueElement.ValueKind == JsonValueKind.Number &&
                maxValueElement.TryGetSingle(out float maxValue))
                dto.MaxValue = maxValue;

            if (root.TryGetProperty(ParameterDtoConstants.EnumDescriptions, out JsonElement enumDescriptionsElement))
            { dto.EnumDescriptions = JsonSerializer.Deserialize<IList<string>>(enumDescriptionsElement.GetRawText(), options); }

            if (root.TryGetProperty(ParameterDtoConstants.EnumLongNames, out JsonElement enumLongNamesElement))
            { dto.EnumLongNames = JsonSerializer.Deserialize<IList<string>>(enumLongNamesElement.GetRawText(), options); }

            if (root.TryGetProperty(ParameterDtoConstants.EnumOptions, out JsonElement enumOptionsElement))
            { dto.EnumOptions = JsonSerializer.Deserialize<IList<TaskModuleParameterEnumOptionCoreDto>>(enumOptionsElement.GetRawText(), options); }

            if (root.TryGetProperty(ParameterDtoConstants.Default, out JsonElement defaultElement))
            {
                var defaultDto = new TaskModuleParameterValueCoreDto();
                if (defaultElement.ValueKind == JsonValueKind.Null)
                {
                    defaultDto = null;
                }
                else if (defaultElement.ValueKind == JsonValueKind.Number)
                {
                    if (defaultElement.TryGetInt32(out int intValue))
                    {
                        defaultDto.IntValue = intValue;
                    }
                    else if (defaultElement.TryGetDouble(out double doubleValue))
                    {
                        defaultDto.FloatValue = (float)doubleValue;
                    }
                }
                else if (defaultElement.ValueKind == JsonValueKind.String)
                {
                    defaultDto.StringValue = defaultElement.GetString();
                }
                else if (defaultElement.ValueKind == JsonValueKind.True ||
                         defaultElement.ValueKind == JsonValueKind.False)
                {
                    defaultDto.BoolValue = defaultElement.GetBoolean();
                }
                else if (defaultElement.ValueKind == JsonValueKind.Array)
                {
                    var listOption = new TaskModuleParameterListOptionCoreDto();
                    foreach (var item in defaultElement.EnumerateArray())
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
                    defaultDto.ListValue = listOption.ParameterValues.Count > 0 ? listOption : null;
                }
                else if (defaultElement.ValueKind == JsonValueKind.Object)
                {
                    defaultDto.EnumValue = JsonSerializer.Deserialize<TaskModuleParameterEnumOptionCoreDto>(defaultElement.GetRawText(), options);
                }

                dto.Default = defaultDto;
            }


            //if (root.TryGetProperty(ParameterDtoConstants.Value, out JsonElement valueElement))
            //{
            //    var valueDto = new TaskModuleParameterValueCoreDto();
            //    if (valueElement.ValueKind == JsonValueKind.Null)
            //    {
            //        valueDto = null;
            //    }
            //    else if (valueElement.ValueKind == JsonValueKind.Number)
            //    {
            //        if (valueElement.TryGetInt32(out int intValue))
            //        {
            //            valueDto.IntValue = intValue;
            //        }
            //        else if (valueElement.TryGetDouble(out double doubleValue))
            //        {
            //            valueDto.FloatValue = (float)doubleValue;
            //        }
            //    }
            //    else if (valueElement.ValueKind == JsonValueKind.String)
            //    {
            //        valueDto.StringValue = valueElement.GetString();
            //    }
            //    else if (valueElement.ValueKind == JsonValueKind.True ||
            //             valueElement.ValueKind == JsonValueKind.False)
            //    {
            //        valueDto.BoolValue = valueElement.GetBoolean();
            //    }
            //    else if (valueElement.ValueKind == JsonValueKind.Array)
            //    {
            //        var listOption = new TaskModuleParameterListOptionCoreDto();
            //        foreach (var item in valueElement.EnumerateArray())
            //        {
            //            var listItem = new Dictionary<string, TaskModuleParameterCoreDto>();
            //            foreach (var property in item.EnumerateObject())
            //            {
            //                var parameter = JsonSerializer.Deserialize<TaskModuleParameterCoreDto>(property.Value.GetRawText(), options);
            //                if (parameter is not null)
            //                {
            //                    listItem[property.Name] = parameter;
            //                }
            //            }
            //            if (listItem.Count > 0)
            //            {
            //                listOption.ParameterValues.Add(listItem);
            //            }
            //        }
            //        valueDto.ListValue = listOption.ParameterValues.Count > 0 ? listOption : null;
            //    }
            //    else if (valueElement.ValueKind == JsonValueKind.Object)
            //    {
            //        valueDto.EnumValue = JsonSerializer.Deserialize<TaskModuleParameterEnumOptionCoreDto>(valueElement.GetRawText(), options);
            //    }

            //    dto.Value = valueDto;
            //}


            if (root.TryGetProperty(ParameterDtoConstants.Readonly, out JsonElement readonlyElement) &&
                (readonlyElement.ValueKind == JsonValueKind.True ||
                 readonlyElement.ValueKind == JsonValueKind.False))
            {
                dto.Readonly = readonlyElement.GetBoolean();
            }

            if (root.TryGetProperty(ParameterDtoConstants.Required, out JsonElement requiredElement) &&
                (requiredElement.ValueKind == JsonValueKind.True ||
                 requiredElement.ValueKind == JsonValueKind.False))
            {
                dto.Required = requiredElement.GetBoolean();
            }

            return dto;
        }

        public override void Write(Utf8JsonWriter writer, TaskModuleParameterCoreDto dto, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString(ParameterDtoConstants.ShortName, dto.ShortName);

            if (!string.IsNullOrEmpty(dto.LongName))
                writer.WriteString(ParameterDtoConstants.LongName, dto.LongName);

            if (!string.IsNullOrEmpty(dto.Description))
                writer.WriteString(ParameterDtoConstants.Description, dto.Description);

            writer.WriteString(ParameterDtoConstants.Type, dto.Type);

            if (dto.MinValue.HasValue)
                writer.WriteNumber(ParameterDtoConstants.MinValue, dto.MinValue.Value);

            if (dto.MaxValue.HasValue)
                writer.WriteNumber(ParameterDtoConstants.MaxValue, dto.MaxValue.Value);

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

            writer.WritePropertyName(ParameterDtoConstants.Default);
            if (dto.Default is not null)
            {
                if (dto.Default.FloatValue.HasValue)
                {
                    writer.WriteNumberValue(dto.Default.FloatValue.Value);
                }
                else if (dto.Default.IntValue.HasValue)
                {
                    writer.WriteNumberValue(dto.Default.IntValue.Value);
                }
                else if (!string.IsNullOrEmpty(dto.Default.StringValue))
                {
                    writer.WriteStringValue(dto.Default.StringValue);
                }
                else if (dto.Default.BoolValue.HasValue)
                {
                    writer.WriteBooleanValue(dto.Default.BoolValue.Value);
                }
                else if (dto.Default.ListValue is not null)
                {
                    JsonSerializer.Serialize(writer, dto.Default.ListValue.ParameterValues, options);
                }
                else if (dto.Default.EnumValue is not null)
                {
                    JsonSerializer.Serialize(writer, dto.Default.EnumValue, options);
                }
                else
                {
                    writer.WriteNullValue();
                }
            }

            //writer.WritePropertyName(ParameterDtoConstants.Value);
            //if (dto.Value is not null)
            //{
            //    if (dto.Value.FloatValue.HasValue)
            //    {
            //        writer.WriteNumberValue(dto.Value.FloatValue.Value);
            //    }
            //    else if (dto.Value.IntValue.HasValue)
            //    {
            //        writer.WriteNumberValue(dto.Value.IntValue.Value);
            //    }
            //    else if (!string.IsNullOrEmpty(dto.Value.StringValue))
            //    {
            //        writer.WriteStringValue(dto.Value.StringValue);
            //    }
            //    else if (dto.Value.BoolValue.HasValue)
            //    {
            //        writer.WriteBooleanValue(dto.Value.BoolValue.Value);
            //    }
            //    else if (dto.Value.ListValue is not null)
            //    {
            //        JsonSerializer.Serialize(writer, dto.Value.ListValue.ParameterValues, options);
            //    }
            //    else if (dto.Value.EnumValue is not null)
            //    {
            //        JsonSerializer.Serialize(writer, dto.Value.EnumValue, options);
            //    }
            //    else
            //    {
            //        writer.WriteNullValue();
            //    }
            //}

            //else
            //{
            //    writer.WriteNullValue();
            //}

            if (dto.Readonly.HasValue)
                writer.WriteBoolean(ParameterDtoConstants.Readonly, dto.Readonly.Value);

            if (dto.Required.HasValue)
                writer.WriteBoolean(ParameterDtoConstants.Required, dto.Required.Value);

            writer.WriteEndObject();
        }
    }
}
