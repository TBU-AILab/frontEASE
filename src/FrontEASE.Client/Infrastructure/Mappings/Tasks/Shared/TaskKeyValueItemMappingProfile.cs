using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Shared;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Shared
{
    public class TaskKeyValueItemMappingProfile : Profile
    {
        public TaskKeyValueItemMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskKeyValueItemDto, TaskKeyValueItemDto>();
        }
    }
}
