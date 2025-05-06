using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List.Params;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options.Parameters.Options.List
{
    public class TaskModuleParameterListOptionParamsMappingProfile : Profile
    {
        public TaskModuleParameterListOptionParamsMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskModuleParameterListOptionParamsDto, TaskModuleParameterListOptionParamsNoValidationDto>()
                .ForMember(x => x.ParameterItems, cd => cd.MapFrom(map => map.ParameterItems))
                .ReverseMap();

            CreateMap<TaskModuleParameterListOptionParamsDto, TaskModuleParameterListOptionParamsDto>()
                .ForMember(x => x.ParameterItems, cd => cd.MapFrom(map => map.ParameterItems));

            CreateMap<TaskModuleParameterListOptionParamsNoValidationDto, TaskModuleParameterListOptionParamsNoValidationDto>()
                .ForMember(x => x.ParameterItems, cd => cd.MapFrom(map => map.ParameterItems));
        }
    }
}
