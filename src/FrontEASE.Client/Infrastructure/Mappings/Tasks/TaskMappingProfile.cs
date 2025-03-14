using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskDto, TaskDto>()
                .ForMember(x => x.Messages, cd => cd.MapFrom(map => map.Messages))
                .ForMember(x => x.Solutions, cd => cd.MapFrom(map => map.Solutions))
                .ForMember(x => x.Author, cd => cd.MapFrom(map => map.Author))
                .ForMember(x => x.Members, cd => cd.MapFrom(map => map.Members))
                .ForMember(x => x.MemberGroups, cd => cd.MapFrom(map => map.MemberGroups))
                .ForMember(x => x.Config, cd => cd.MapFrom(map => map.Config));
        }
    }
}
