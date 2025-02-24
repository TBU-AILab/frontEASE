using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Configs.Modules.Options;

namespace FoP_IMT.Client.Infrastructure.Mappings.Tasks.Configs.ConfigParts.Modules.Options
{
    public class TaskModuleMappingProfile : Profile
    {
        public TaskModuleMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskModuleDto, TaskModuleDto>()
                .ForMember(x => x.Parameters, cd => cd.MapFrom(map => map.Parameters));

            CreateMap<TaskModuleDto, TaskModuleNoValidationDto>()
                .ForMember(x => x.Parameters, cd => cd.MapFrom(map => map.Parameters))
                .ReverseMap();
        }
    }
}
