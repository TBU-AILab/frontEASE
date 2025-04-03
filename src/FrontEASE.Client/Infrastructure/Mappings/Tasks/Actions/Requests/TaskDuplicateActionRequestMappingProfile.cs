using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Actions.Requests;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Actions.Requests
{
    public class TaskDuplicateActionRequestMappingProfile : Profile
    {
        public TaskDuplicateActionRequestMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskDuplicateActionRequestDto, TaskDuplicateActionRequestDto>();
        }
    }
}
