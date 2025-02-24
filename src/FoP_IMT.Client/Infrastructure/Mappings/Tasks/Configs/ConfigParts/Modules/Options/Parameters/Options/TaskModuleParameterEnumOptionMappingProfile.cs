using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;

namespace FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Options
{
    public class TaskModuleParameterEnumOptionMappingProfile : Profile
    {
        public TaskModuleParameterEnumOptionMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskModuleParameterEnumOptionDto, TaskModuleParameterEnumOptionNoValidationDto>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ForMember(x => x.Metadata, cd => cd.MapFrom(map => map.Metadata))
                .ReverseMap();

            CreateMap<TaskModuleParameterEnumOptionDto, TaskModuleParameterEnumOptionDto>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ForMember(x => x.Metadata, cd => cd.MapFrom(map => map.Metadata));

            CreateMap<TaskModuleParameterEnumOptionNoValidationDto, TaskModuleParameterEnumOptionNoValidationDto>()
                .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
                .ForMember(x => x.Metadata, cd => cd.MapFrom(map => map.Metadata));
        }
    }
}