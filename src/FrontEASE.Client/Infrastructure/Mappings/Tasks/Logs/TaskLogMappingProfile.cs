using AutoMapper;
using FrontEASE.Shared.Data.DTOs.Tasks.Data.Logs;

namespace FrontEASE.Client.Infrastructure.Mappings.Tasks.Logs
{
    public class TaskLogMappingProfile : Profile
    {
        public TaskLogMappingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<TaskLogDto, TaskLogDto>();
        }
    }
}
