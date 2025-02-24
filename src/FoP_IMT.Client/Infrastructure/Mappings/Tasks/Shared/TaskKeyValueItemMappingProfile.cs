using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Shared;

namespace FoP_IMT.Client.Infrastructure.Mappings.Tasks.Shared
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
