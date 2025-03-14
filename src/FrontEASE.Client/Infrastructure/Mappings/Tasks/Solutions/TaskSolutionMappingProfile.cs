using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Solutions;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Solutions
{
    public class TaskSolutionMappingProfile : Profile
    {
        public TaskSolutionMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskSolutionDto, TaskSolutionDto>();
        }
    }
}
