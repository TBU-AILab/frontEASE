using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks;
using FoP_IMT.Shared.Data.Enums.Tasks.Config;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks
{
    public class TaskInfoMappingProfile : Profile
    {
        public TaskInfoMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Domain.Entities.Tasks.Task, TaskInfoDto>()
                .ForMember(x => x.Name, cd => cd.MapFrom(map => map.Config.Name))
                .ForMember(x => x.Model, cd => cd.MapFrom(map =>
                    map.Config.Modules != null && map.Config.Modules.Any(m => m.PackageType == ModuleType.LLM_CONNECTOR) ?
                    map.Config.Modules.First(m => m.PackageType == ModuleType.LLM_CONNECTOR).ShortName : string.Empty))
                .ForMember(x => x.SolutionType, cd => cd.MapFrom(map =>
                    map.Config.Modules != null && map.Config.Modules.Any(m => m.PackageType == ModuleType.SOLUTION) ?
                    map.Config.Modules.First(m => m.PackageType == ModuleType.SOLUTION).ShortName : string.Empty))
                .ForMember(x => x.Author, cd => cd.MapFrom(map => map.Members.FirstOrDefault(m => m.Id == map.AuthorID.ToString())))
                .ReverseMap();
        }
    }
}
