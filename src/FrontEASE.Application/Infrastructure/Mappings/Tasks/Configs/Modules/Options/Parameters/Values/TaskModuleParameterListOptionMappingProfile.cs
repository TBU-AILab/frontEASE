using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Data.Configs.Modules.Values.Parameters.Options;
using FrontEASE.Domain.Entities.Tasks.Configs.Modules.Options.Parameters.Options;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options.Parameters.Options;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Configs.Modules.Options.Parameters.Values
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
                .ForMember(x => x.ParameterValues, cd => cd.MapFrom(map => map.ParameterValues))
                .ReverseMap();

            CreateMap<TaskModuleParameterListOption, TaskModuleParameterListOptionNoValidationDto>()
                .ForMember(x => x.ParameterValues, cd => cd.MapFrom(map => map.ParameterValues))
                .ReverseMap();

            //CreateMap<TaskModuleParameterListOptionEntity, TaskModuleParameterListOptionDto>()
            //    .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
            //    .ReverseMap();

            //CreateMap<TaskModuleParameterListOptionEntity, TaskModuleParameterListOptionNoValidationDto>()
            //    .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue))
            //    .ReverseMap();
        }

        private void CreateMapsEntities()
        {
            CreateMap<TaskModuleParameterListOption, TaskModuleParameterListOption>()
                .ForMember(x => x.ParameterValues, cd => cd.MapFrom(map => map.ParameterValues));

            //CreateMap<TaskModuleParameterListOptionEntity, TaskModuleParameterListOptionEntity>()
            //    .ForMember(x => x.ID, cd => cd.Ignore())
            //    .ForMember(x => x.ModuleValueID, cd => cd.Ignore())
            //    .ForMember(x => x.ParameterValue, cd => cd.Ignore())

            //    .ForMember(x => x.ModuleValue, cd => cd.MapFrom(map => map.ModuleValue));
        }

        private void CreateMapsCore()
        {
            CreateMap<TaskModuleParameterListOption, TaskModuleParameterListOptionCoreDto>()
                .ForMember(x => x.ParameterValues, cd => cd.MapFrom(map => map.ParameterValues))
                .ReverseMap();

            //CreateMap<TaskModuleParameterListOptionEntity, TaskModuleParameterListOptionCoreDto>()
            //    .ForMember(x => x.ParameterValues, cd => cd.MapFrom(map => map.ParameterValues))
            //    .ReverseMap();
        }
    }
}
