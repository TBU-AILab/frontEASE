using AutoMapper;
using FrontEASE.DataContracts.Models.Core.Tasks.Info;
using FrontEASE.Shared.Data.DTOs.Tasks.UI;

namespace FrontEASE.Application.Infrastructure.Mappings.Tasks
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
