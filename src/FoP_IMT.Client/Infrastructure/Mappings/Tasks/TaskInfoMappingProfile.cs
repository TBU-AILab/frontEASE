using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data;

namespace FoP_IMT.Client.Infrastructure.Mappings.Tasks
{
    public class TaskInfoMappingProfile : Profile
    {
        public TaskInfoMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskInfoDto, TaskInfoDto>()
                .ForMember(x => x.Author, cd => cd.MapFrom(map => map.Author));

            CreateMap<TaskDto, TaskInfoDto>()
                .ForMember(x => x.Name, cd => cd.MapFrom(map => map.Config.Name))
                .ForMember(x => x.Author, cd => cd.MapFrom(map => map.Author))
                .ForMember(x => x.ConnectorType, cd => cd.MapFrom(map => map.Config.Connector.ShortName))
                .ForMember(x => x.SolutionType, cd => cd.MapFrom(map => map.Config.Solution.ShortName));

        }
    }
}
