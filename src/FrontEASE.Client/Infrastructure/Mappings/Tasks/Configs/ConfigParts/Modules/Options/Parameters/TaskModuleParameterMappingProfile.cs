using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters
{
    public class TaskModuleParameterMappingProfile : Profile
    {
        public TaskModuleParameterMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskModuleParameterNoValidationDto, TaskModuleParameterDto>()
                .ReverseMap();

            CreateMap<TaskModuleParameterDto, TaskModuleParameterDto>()
                .ForMember(x => x.Value, cd => cd.MapFrom(map => map.Value));

            CreateMap<TaskModuleParameterNoValidationDto, TaskModuleParameterNoValidationDto>()
                .ForMember(x => x.EnumDescriptions, cd => cd.MapFrom(map => map.EnumDescriptions))
                .ForMember(x => x.EnumLongNames, cd => cd.MapFrom(map => map.EnumLongNames))
                .ForMember(x => x.EnumOptions, cd => cd.MapFrom(map => map.EnumOptions))
                .ForMember(x => x.Default, cd => cd.MapFrom(map => map.Default));
        }
    }
}
