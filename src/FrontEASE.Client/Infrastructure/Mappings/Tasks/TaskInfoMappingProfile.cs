using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks
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
