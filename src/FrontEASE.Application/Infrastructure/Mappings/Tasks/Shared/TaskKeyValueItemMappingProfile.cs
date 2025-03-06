using AutoMapper;
using FrontEASE.Domain.Entities.Tasks.Shared;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Shared;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks.Shared
{
    public class TaskKeyValueItemMappingProfile : Profile
    {
        public TaskKeyValueItemMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskKeyValueItem, TaskKeyValueItemDto>()
                .ReverseMap();

            CreateMap<TaskKeyValueItem, TaskKeyValueItem>();
        }
    }
}
