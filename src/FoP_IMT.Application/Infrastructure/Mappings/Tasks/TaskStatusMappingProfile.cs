using AutoMapper;
using FoP_IMT.DataContracts.Models.Core.Tasks.Info;
using FoP_IMT.Shared.Data.DTOs.Tasks.UI;

namespace FoP_IMT.Application.Infrastructure.Mappings.Tasks
{
    public class TaskStatusMappingProfile : Profile
    {
        public TaskStatusMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Domain.Entities.Tasks.Task, TaskStatusDto>();

            CreateMap<TaskInfoCoreDto, Domain.Entities.Tasks.Task>().ReverseMap();
        }
    }
}
