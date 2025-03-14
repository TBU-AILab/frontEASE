using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Values;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters
{
    public class TaskModuleParameterValueMappingProfile : Profile
    {
        public TaskModuleParameterValueMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskModuleParameterValueNoValidationDto, TaskModuleParameterValueDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.Metadata, cd => cd.MapFrom(map => map.Metadata))
                .ReverseMap();

            CreateMap<TaskModuleParameterValueDto, TaskModuleParameterValueDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.Metadata, cd => cd.MapFrom(map => map.Metadata));

            CreateMap<TaskModuleParameterValueNoValidationDto, TaskModuleParameterValueNoValidationDto>()
                .ForMember(x => x.EnumValue, cd => cd.MapFrom(map => map.EnumValue))
                .ForMember(x => x.Metadata, cd => cd.MapFrom(map => map.Metadata));
        }
    }
}
