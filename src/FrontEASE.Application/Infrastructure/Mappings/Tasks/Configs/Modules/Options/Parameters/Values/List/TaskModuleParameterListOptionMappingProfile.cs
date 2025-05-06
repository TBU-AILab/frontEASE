using AutoMapper;
using FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.List.Converters;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options.List;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options.List;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values.List
{
    public class TaskModuleParameterListOptionMappingProfile : Profile
    {
        public TaskModuleParameterListOptionMappingProfile()
        {
            CreateMapsDtos();
            CreateMapsEntities();
            CreateMapsCore();
        }

        private void CreateMapsDtos()
        {
            CreateMap<TaskModuleParameterListOption, TaskModuleParameterListOptionDto>()
                .ForMember(x => x.ParameterValues, opt => opt.MapFrom(src => src.ParameterValues))
                .ReverseMap();

            CreateMap<TaskModuleParameterListOption, TaskModuleParameterListOptionNoValidationDto>()
                .ForMember(x => x.ParameterValues, opt => opt.MapFrom(src => src.ParameterValues))
                .ReverseMap();
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskModuleParameterListOption, TaskModuleParameterListOption>()
                .ForMember(x => x.ParameterValues, opt => opt.MapFrom(src => src.ParameterValues));
        }

        private void CreateMapsCore()
        {
            CreateMap<TaskModuleParameterListOptionCoreDto, TaskModuleParameterListOption>()
                .ConvertUsing(new TaskModuleParameterListOptionCoreDtoConverter());

            CreateMap<TaskModuleParameterListOption, TaskModuleParameterListOptionCoreDto>()
                .ConvertUsing(new TaskModuleParameterListOptionCoreDtoReverseConverter());
        }
    }
}
