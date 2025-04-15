using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Options
{
    public class TaskModuleParameterListOptionMappingProfile : Profile
    {
        public TaskModuleParameterListOptionMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskModuleParameterListOptionDto, TaskModuleParameterListOptionNoValidationDto>()
                .ForMember(x => x.ParameterValues, cd => cd.MapFrom(map => map.ParameterValues))
                .ReverseMap();

            CreateMap<TaskModuleParameterListOptionDto, TaskModuleParameterListOptionDto>()
                .ForMember(x => x.ParameterValues, cd => cd.MapFrom(map => map.ParameterValues));

            CreateMap<TaskModuleParameterListOptionNoValidationDto, TaskModuleParameterListOptionNoValidationDto>()
                .ForMember(x => x.ParameterValues, cd => cd.MapFrom(map => map.ParameterValues));
        }
    }
}
