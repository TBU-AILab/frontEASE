using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Actions.Requests
{
    public class TaskFilterActionRequestMappingProfile : Profile
    {
        public TaskFilterActionRequestMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskFilterActionRequestDto, TaskFilterActionRequestDto>()
                 .ForMember(x => x.State, cd => cd.MapFrom(map => map.State));
        }
    }
}
