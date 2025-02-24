using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks.UI;

namespace FoP_IMT.Client.Infrastructure.Mappings.Tasks
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
