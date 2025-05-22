using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Values;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values
{
    public class TaskModuleParameterValueMappingProfile : Profile
    {
        public TaskModuleParameterValueMappingProfile()
        {
            CreateMapsDtos();
            CreateMapsEntities();
            CreateMapsCore();
        }

        private void CreateMapsDtos()
        {
            CreateMap<TaskModuleParameterValue, TaskModuleParameterValueDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.ListValue, cd => cd.MapFrom(map => map.ListValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterValue, TaskModuleParameterValueNoValidationDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.ListValue, cd => cd.MapFrom(map => map.ListValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterValueEntity, TaskModuleParameterValueDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.ListValue, cd => cd.MapFrom(map => map.ListValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterValueEntity, TaskModuleParameterValueNoValidationDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.ListValue, cd => cd.MapFrom(map => map.ListValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterValueEntity, TaskModuleParameterValue>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.ListValue, cd => cd.MapFrom(map => map.ListValue))
                .ReverseMap();
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskModuleParameterValue, TaskModuleParameterValue>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.ListValue, cd => cd.MapFrom(map => map.ListValue));

            CreateMap<TaskModuleParameterValueEntity, TaskModuleParameterValueEntity>()
                .ForMember(x => x.ID, cd => cd.Ignore())
                .ForMember(x => x.EnumValueID, cd => cd.Ignore())
                .ForMember(x => x.ListValueID, cd => cd.Ignore())
                .ForMember(x => x.Parameter, cd => cd.Ignore())

                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.ListValue, cd => cd.MapFrom(map => map.ListValue));
        }

        private void CreateMapsCore()
        {
            CreateMap<TaskModuleParameterValue, TaskModuleParameterValueCoreDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.ListValue, cd => cd.MapFrom(map => map.ListValue))
                .ReverseMap();

            CreateMap<TaskModuleParameterValueEntity, TaskModuleParameterValueCoreDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.ListValue, cd => cd.MapFrom(map => map.ListValue))
                .ReverseMap();
        }
    }
}
