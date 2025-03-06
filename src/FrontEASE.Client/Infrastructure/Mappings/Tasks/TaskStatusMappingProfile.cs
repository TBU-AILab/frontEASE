using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks
{
    public class TaskStatusMappingProfile : Profile
    {
        public TaskStatusMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskStatusDto, TaskStatusDto>();
        }
    }
}
