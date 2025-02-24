using AutoMapper;
using FoP_IMT.DataContracts.Models.Core.Shared;
using FoP_IMT.Domain.Entities.Tasks.Shared;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Shared;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks.Shared
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

            CreateMap<TaskKeyValueItem, TaskKeyValueItemCoreDto>().ReverseMap();
        }
    }
}
