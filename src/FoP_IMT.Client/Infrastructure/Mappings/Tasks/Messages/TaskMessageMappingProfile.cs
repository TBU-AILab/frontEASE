using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Messages;

namespace FoP_IMT.Client.Infrastructure.Mappings.Tasks.Messages
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
