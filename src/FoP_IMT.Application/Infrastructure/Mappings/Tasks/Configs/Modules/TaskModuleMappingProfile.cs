using AutoMapper;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules;
using FoP_IMT.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;
using FoP_IMT.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks.Configs.Modules
{
    public class TaskModuleMappingProfile : Profile
    {
        public TaskModuleMappingProfile()
        {
            CreateMapsDtos();
            CreateMapsEntities();
            CreateMapsCore();
        }

        private void CreateMapsDtos()
        {
            CreateMap<TaskModule, TaskModuleDto>()
                .ForMember(x => x.Parameters, cd => cd.MapFrom(map => map.Parameters))
                .ReverseMap();
            CreateMap<TaskModule, TaskModuleNoValidationDto>()
                .ForMember(x => x.Parameters, cd => cd.MapFrom(map => map.Parameters))
                .ReverseMap();
            CreateMap<TaskModuleEntity, TaskModuleDto>()
                .ForMember(x => x.Parameters, cd => cd.MapFrom(map => map.Parameters))
                .ReverseMap();
            CreateMap<TaskModuleEntity, TaskModuleNoValidationDto>()
                .ForMember(x => x.Parameters, cd => cd.MapFrom(map => map.Parameters))
                .ReverseMap();
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskModule, TaskModule>()
                .ForMember(x => x.Parameters, cd => cd.MapFrom(map => map.Parameters));

            CreateMap<TaskModuleEntity, TaskModuleEntity>()
                .ForMember(x => x.Parameters, cd => cd.MapFrom(map => map.Parameters))

                .ForMember(x => x.ID, cd => cd.Ignore())
                .ForMember(x => x.TaskConfig, cd => cd.Ignore())
                .ForMember(x => x.TaskConfigID, cd => cd.Ignore())
                .ForMember(x => x.ParameterEnumValue, cd => cd.Ignore());
        }

        private void CreateMapsCore()
        {
            CreateMap<TaskModuleParameterValue, TaskModuleParameterCoreDto>()
                .ForMember(dto => dto.Default, opt => opt.MapFrom(src => src))
                .ReverseMap();

            CreateMap<TaskModule, TaskModuleCoreDto>()
                .ForMember(
                    dto => dto.Parameters,
                    opt => opt.MapFrom((src, dest, destMember, context) =>
                        src.Parameters.ToDictionary(
                            param => param.Key,
                            param => context.Mapper.Map<TaskModuleCoreDto>(param)))
                );

            CreateMap<TaskModuleCoreDto, TaskModule>()
                .ForMember(
                    entity => entity.Parameters,
                    opt => opt.MapFrom((src, dest, destMember, context) =>
                        src.Parameters.Select(kvp => new TaskModuleParameter
                        {
                            Key = kvp.Key,
                            ShortName = kvp.Value.ShortName,
                            LongName = kvp.Value.LongName,
                            Description = kvp.Value.Description,
                            Type = kvp.Value.Type,
                            MinValue = kvp.Value.MinValue,
                            MaxValue = kvp.Value.MaxValue,
                            EnumDescriptions = kvp.Value.EnumDescriptions,
                            EnumLongNames = kvp.Value.EnumLongNames,
                            EnumOptions = kvp.Value.EnumOptions?.Select(opt => context.Mapper.Map<TaskModuleParameterEnumOption>(opt))?.ToList() ?? [],
                            Default = kvp.Value.Default is not null ? context.Mapper.Map<TaskModuleParameterValue>(kvp.Value.Default) : null,
                            Readonly = kvp.Value.Readonly,
                            Required = kvp.Value.Required
                        }).ToList())
                );

            CreateMap<TaskModuleCoreDto, TaskModuleEntity>()
            .ForMember(
                entity => entity.Parameters,
                opt => opt.MapFrom((src, dest, destMember, context) =>
                    src.Parameters.Select(kvp => new TaskModuleParameterEntity
                    {
                        Key = kvp.Key,
                        ShortName = kvp.Value.ShortName,
                        Type = kvp.Value.Type,
                    }).ToList())
            );

            CreateMap<TaskModule, TaskModuleInputCoreDto>()
                .ForMember(
                    dto => dto.Parameters,
                    opt => opt.MapFrom((src, dest, destMember, context) =>
                        src.Parameters.ToDictionary(
                            param => param.Key,
                            param => MapParameterValue(param.Value, context)
                        )
                    )
                )
                .ReverseMap()
                .ForMember(
                    entity => entity.Parameters,
                    opt => opt.MapFrom((src, dest, destMember, context) =>
                        src.Parameters.Select(kvp => new TaskModuleParameter
                        {
                            Key = kvp.Key,
                            Value = MapParameterValueReverse(kvp.Value, context)
                        }).ToList()
                    )
                );

            CreateMap<TaskModuleEntity, TaskModuleInputCoreDto>()
                .ForMember(
                    dto => dto.Parameters,
                    opt => opt.MapFrom((src, dest, destMember, context) =>
                        src.Parameters.ToDictionary(
                            param => param.Key,
                            param => MapParameterValueEntity(param.Value, context)
                        )
                    )
                )
                .ReverseMap()
                .ForMember(
                    entity => entity.Parameters,
                    opt => opt.MapFrom((src, dest, destMember, context) =>
                        src.Parameters.Select(kvp => new TaskModuleParameterEntity
                        {
                            Key = kvp.Key,
                            Value = MapParameterValueReverseEntity(kvp.Value, context)
                        }).ToList()
                    )
                );
        }


        #region Helpers

        private static object? MapParameterValue(TaskModuleParameterValue? value, ResolutionContext context)
        {
            if (value is null)
            { return null; }

            if (value.EnumValue is not null)
            {
                if (value.EnumValue.ModuleValue is not null)
                { return context.Mapper.Map<TaskModuleInputCoreDto>(value.EnumValue.ModuleValue); }

                if (!string.IsNullOrEmpty(value.EnumValue.StringValue))
                { return value.EnumValue.StringValue; }
            }

            return value.StringValue ?? (object?)value.IntValue ?? (object?)value.FloatValue ?? value.BoolValue;
        }

        private static object? MapParameterValueEntity(TaskModuleParameterValueEntity? value, ResolutionContext context)
        {
            if (value is null)
            { return null; }

            if (value.EnumValue is not null)
            {
                if (value.EnumValue.ModuleValue is not null)
                { return context.Mapper.Map<TaskModuleInputCoreDto>(value.EnumValue.ModuleValue); }

                if (!string.IsNullOrEmpty(value.EnumValue.StringValue))
                { return value.EnumValue.StringValue; }
            }

            return value.StringValue ?? (object?)value.IntValue ?? (object?)value.FloatValue ?? value.BoolValue;
        }


        private static TaskModuleParameterValue? MapParameterValueReverse(object? value, ResolutionContext context)
        {
            if (value is null)
            { return null; }

            if (value is TaskModuleInputCoreDto nestedModuleDto)
            {
                return new TaskModuleParameterValue
                {
                    EnumValue = new TaskModuleParameterEnumOption
                    {
                        ModuleValue = context.Mapper.Map<TaskModule>(nestedModuleDto)
                    }
                };
            }

            if (value is string stringValue)
            {
                return new TaskModuleParameterValue
                {
                    EnumValue = new TaskModuleParameterEnumOption
                    {
                        StringValue = stringValue
                    }
                };
            }

            return value switch
            {
                int intVal => new TaskModuleParameterValue { IntValue = intVal },
                float floatVal => new TaskModuleParameterValue { FloatValue = floatVal },
                bool boolVal => new TaskModuleParameterValue { BoolValue = boolVal },
                _ => null
            };
        }

        private static TaskModuleParameterValueEntity? MapParameterValueReverseEntity(object? value, ResolutionContext context)
        {
            if (value is null)
            { return null; }

            if (value is TaskModuleInputCoreDto nestedModuleDto)
            {
                return new TaskModuleParameterValueEntity
                {
                    EnumValue = new TaskModuleParameterEnumValueEntity
                    {
                        ModuleValue = context.Mapper.Map<TaskModuleEntity>(nestedModuleDto)
                    }
                };
            }

            if (value is string stringValue)
            {
                return new TaskModuleParameterValueEntity
                {
                    EnumValue = new TaskModuleParameterEnumValueEntity
                    {
                        StringValue = stringValue
                    }
                };
            }

            return value switch
            {
                int intVal => new TaskModuleParameterValueEntity { IntValue = intVal },
                float floatVal => new TaskModuleParameterValueEntity { FloatValue = floatVal },
                bool boolVal => new TaskModuleParameterValueEntity { BoolValue = boolVal },
                _ => null
            };
        }

        #endregion
    }
}
