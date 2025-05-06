using AutoMapper;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List.Params;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.List
{
    public class TaskModuleParameterListOptionParamsMappingProfile : Profile
    {
        public TaskModuleParameterListOptionParamsMappingProfile()
        {
            CreateMapsDtos();
            CreateMapsEntities();
        }

        private void CreateMapsDtos()
        {
            CreateMap<TaskModuleParameterListOptionParams, TaskModuleParameterListOptionParamsDto>()
                .ForMember(x => x.ParameterItems, opt => opt.MapFrom(src => src.ParameterItems))
                .ReverseMap();

            CreateMap<TaskModuleParameterListOptionParams, TaskModuleParameterListOptionParamsNoValidationDto>()
                .ForMember(x => x.ParameterItems, opt => opt.MapFrom(src => src.ParameterItems))
                .ReverseMap();
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskModuleParameterListOptionParams, TaskModuleParameterListOptionParams>()
                .ForMember(x => x.ParameterItems, opt => opt.MapFrom(src => src.ParameterItems));
        }
    }
}
