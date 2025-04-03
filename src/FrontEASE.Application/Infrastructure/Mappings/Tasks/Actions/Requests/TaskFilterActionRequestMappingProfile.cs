using AutoMapper;
using FrontEASE.Domain.Entities.Tasks.Actions.Filtering;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Actions.Requests
{
    public class TaskFilterActionRequestMappingProfile : Profile
    {
        public TaskFilterActionRequestMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskFilterActionRequestDto, TaskFilterActionRequest>()
                .ForMember(x => x.State, cd => cd.MapFrom(map => map.State));
        }
    }
}
