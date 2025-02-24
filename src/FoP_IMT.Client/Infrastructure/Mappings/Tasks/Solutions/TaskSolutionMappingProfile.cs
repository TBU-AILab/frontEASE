using AutoMapper;
using FoP_IMT.Shared.Data.DTOs.Tasks.Data.Solutions;

namespace FoP_IMT.Client.Infrastructure.Mappings.Tasks.Solutions
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
