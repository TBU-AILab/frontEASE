using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Messages;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Messages
{
    public class TaskMessageMappingProfile : Profile
    {
        public TaskMessageMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskMessageDto, TaskMessageDto>();
        }
    }
}
